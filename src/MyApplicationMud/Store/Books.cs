using MyApplicationMud.Shared;
using System.Reactive.Linq;

namespace MyApplicationMud.Store;

public record BooksState
{
    public bool IsLoading { get; init; }
    public bool HasErrors => Errors.Any();
    public IList<IBookListInfo> Items { get; init; } = new List<IBookListInfo>();
    public IEnumerable<IClientError> Errors { get; init; } = Enumerable.Empty<IClientError>();

    public int? BookId { get; init; }
    public BookModelInput? EditBook { get; init; }

    public IEnumerable<IAuthorInfo> Authors { get; init; } = Array.Empty<IAuthorInfo>();

    public DialogReference? CurrentDialog { get; init; }
}

public class BooksFeature : Feature<BooksState>
{
    public override string GetName() => nameof(BooksState);

    protected override BooksState GetInitialState()
        => new BooksState
        {
            IsLoading = true
        };
}

public record BooksLoadingAction();
public record BookChangedAction(IBookListInfo Book);
public record BookAddedAction(IBookListInfo Book);
public record BookDeletedAction(int BookId);

public record RefreshBooksAction();

public record BooksLoadedAction(IEnumerable<IBookListInfo> Items);

public record BooksLoadedWithClientErrorsAction(IEnumerable<IClientError> Errors);

public record SaveBookAction(int? BookId, BookModelInput BookModel);
public record DeleteBookAction(int BookId);

public class RefreshBooksEffect : Effect<RefreshBooksAction>, IDisposable
{
    IDisposable? Subscription;
    public IMyApplicationMudClient Client { get; }
    public ISnackbar Snackbar { get; set; }

    public RefreshBooksEffect(IMyApplicationMudClient Client, ISnackbar Snackbar)
        => (this.Client, this.Snackbar) = (Client, Snackbar);

    public async override Task HandleAsync(RefreshBooksAction action, IDispatcher dispatcher)
    {
        Dispose();

        var result = await Client
           .GetBooksListView
           .ExecuteAsync();

        dispatcher.Dispatch(new BooksLoadedWithClientErrorsAction(result.Errors));
        if (result.Data is not null)
        {
            dispatcher.Dispatch(new BooksLoadedAction(result.Data.Items));
            Snackbar.Add("Daten wurden aktualisiert", Severity.Success);
        }

        Subscription = Client
           .BooksChangedSubscription
           .Watch(ExecutionStrategy.CacheAndNetwork)
           .Catch((Exception e) =>
           {
               dispatcher.Dispatch(new UnexpectedExceptionExceptionAction(e));
               return Observable.Empty<IOperationResult<IBooksChangedSubscriptionResult>>();
           })
           .Subscribe(result =>
           {
               dispatcher.Dispatch(new BooksLoadedWithClientErrorsAction(result.Errors));
               if (result.Data is not null)
               {
                   var action = result.Data.Changed.Type switch
                   {
                       ChangeType.Added => (object)new BookAddedAction(result.Data.Changed.Book),
                       ChangeType.Modified => (object)new BookChangedAction(result.Data.Changed.Book),
                       ChangeType.Deleted => (object)new BookDeletedAction(result.Data.Changed.Book.Id),
                       ChangeType val => throw new ArgumentOutOfRangeException(nameof(ChangeType), val, "Unknown Change Type")
                   };

                   dispatcher.Dispatch(action);
                   Snackbar.Add("Daten wurden aktualisiert", Severity.Success);
               }
           });
    }

    public void Dispose()
        => Subscription?.Dispose();
}

public record AddBookAction();
public record EditBookAction(int BookId);
public record EditBookFetchedAction(int BookId, BookModelInput BookModel);

public record AuthorsFetchedAction(IEnumerable<IAuthorInfo> Authors);
public record ShowDialogAction(DialogReference DialogReference);
public record CloseDialogAction(DialogReference DialogReference);
public record RemoveDialogAction();

public static class BooksReducer
{
    [ReducerMethod(typeof(BooksLoadingAction))]
    public static BooksState BooksLoadingReducer(BooksState booksState)
        => booksState with
        {
            IsLoading = true
        };

    [ReducerMethod(typeof(RefreshBooksAction))]
    public static BooksState RefreshBooksReducer(BooksState booksState)
        => booksState with
        {
            IsLoading = true
        };

    [ReducerMethod]
    public static BooksState BooksLoadedReducer(BooksState booksState, BooksLoadedAction booksLoadedAction)
        => booksState with
        {
            IsLoading = false,
            Items = booksLoadedAction.Items.ToList()
        };

    [ReducerMethod]
    public static BooksState BooksLoadedWithErrorsReducer(BooksState booksState, BooksLoadedWithClientErrorsAction booksLoadedWithClientErrorsAction)
        => booksState with
        {
            IsLoading = false,
            Errors = booksLoadedWithClientErrorsAction.Errors
        };

    [ReducerMethod(typeof(AddBookAction))]
    public static BooksState AddBookReducer(BooksState booksState)
        => booksState with
        {
            EditBook = new BookModelInput()
        };

    [ReducerMethod]
    public static BooksState BookChangedReducer(BooksState state, BookChangedAction action)
    {
        var oldBook = state.Items.FirstOrDefault(m => m.Id == action.Book.Id);
        if (oldBook is not null)
        {
            var items = state.Items.ToList();
            var index = items.IndexOf(oldBook);
            items.Insert(index, action.Book);
            items.Remove(oldBook);
            return state with { Items = items };
        }
        return state;
    }

    [ReducerMethod]
    public static BooksState BookAddedReducer(BooksState state, BookAddedAction action)
    {
        var oldBook = state.Items.FirstOrDefault(m => m.Id == action.Book.Id);
        if (oldBook is null)
        {
            var items = state.Items.ToList();
            items.Add(action.Book);
            return state with { Items = items };
        }
        return state;
    }

    [ReducerMethod]
    public static BooksState BookAddedReducer(BooksState state, BookDeletedAction action)
    {
        var oldBook = state.Items.FirstOrDefault(m => m.Id == action.BookId);
        if (oldBook is not null)
        {
            var items = state.Items.ToList();
            items.Remove(oldBook);
            return state with { Items = items };
        }
        return state;
    }

    [ReducerMethod]
    public static BooksState AuthorsFetechedReducer(BooksState state, AuthorsFetchedAction action)
        => state with { Authors = action.Authors };

    [ReducerMethod]
    public static BooksState EditBookFetchedReducer(BooksState state, EditBookFetchedAction action)
        => state with { BookId = action.BookId, EditBook = action.BookModel };


    [ReducerMethod]
    public static BooksState ShowDialogReducer(BooksState state, ShowDialogAction action)
        => state with { CurrentDialog = action.DialogReference };

    [ReducerMethod(typeof(RemoveDialogAction))]
    public static BooksState RemoveDialogReducer(BooksState state)
        => state with { CurrentDialog = null };

    [ReducerMethod(typeof(CloseDialogAction))]
    public static BooksState ShowDialogReducer(BooksState state)
        => state with { CurrentDialog = null, BookId = null, EditBook = null };
}

public record BookEffects(IMyApplicationMudClient Client, IDialogService DialogService, IState<BooksState> State)
{
    [EffectMethod]
    public Task HandleCloseDialogEffect(CloseDialogAction action, IDispatcher dispatcher)
    {
        DialogService.Close(action.DialogReference);
        dispatcher.Dispatch(new RemoveDialogAction());

        return Task.CompletedTask;
    }

    [EffectMethod(typeof(AddBookAction))]
    public async Task HandleAddBookAction(IDispatcher dispatcher)
    {
        var authors = await Client.GetBookAuthors.ExecuteAsync();

        if (authors.IsSuccessResult() && authors.Data is not null)
        {
            dispatcher.Dispatch(new AuthorsFetchedAction(authors.Data.Authors));
        }

        var dialog = DialogService.Show<BooksDialog>("Neues Buch", new DialogOptions()
        {
            CloseOnEscapeKey = true,
            Position = DialogPosition.TopCenter,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true
        });

        dispatcher.Dispatch(new ShowDialogAction((DialogReference)dialog));
    }

    [EffectMethod]
    public async Task HandleEditBookAction(EditBookAction action, IDispatcher dispatcher)
    {
        var authors = await Client.GetBooksDetailView.ExecuteAsync(action.BookId);

        if (authors.IsSuccessResult() && authors.Data is not null)
        {
            dispatcher.Dispatch(new AuthorsFetchedAction(authors.Data.Authors));
            dispatcher.Dispatch(new EditBookFetchedAction(authors.Data.Details.Id, new BookModelInput()
            {
                AuthorId = authors.Data.Details.Author.Id,
                Title = authors.Data.Details.Title
            }));
        }

        var dialog = DialogService.Show<BooksDialog>("Buch bearbeiten", new DialogOptions()
        {
            CloseOnEscapeKey = true,
            Position = DialogPosition.TopCenter,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true
        });

        dispatcher.Dispatch(new ShowDialogAction((DialogReference)dialog));
    }

    [EffectMethod]
    public async Task HandleSaveBookAction(SaveBookAction action, IDispatcher dispatcher)
    {
        if (action.BookId.HasValue)
        {
            var result = await Client.UpdateBook.ExecuteAsync(action.BookId.Value.ToString(), action.BookModel);
            try
            {
                result.EnsureNoErrors();
            }
            catch (Exception ex)
            {

                dispatcher.Dispatch(new UnexpectedExceptionExceptionAction(ex));
            }
        }
        else
        {
            //TODO: strawberry shake does not format body correctly
            var result = await Client.AddBook.ExecuteAsync(new BookModelInput
            {
                Title = action.BookModel.Title,
                AuthorId = action.BookModel.AuthorId
            });
            try
            {
                result.EnsureNoErrors();
            }
            catch (Exception ex)
            {

                dispatcher.Dispatch(new UnexpectedExceptionExceptionAction(ex));
            }
        }

        dispatcher.Dispatch(new CloseDialogAction(State.Value.CurrentDialog!));
    }

    [EffectMethod]
    public async Task HandleDeleteBookAction(DeleteBookAction action, IDispatcher dispatcher)
    {
        var result = await Client.DeleteBook.ExecuteAsync(action.BookId.ToString());
        try
        {
            result.EnsureNoErrors();
        }
        catch (Exception ex)
        {
            dispatcher.Dispatch(new UnexpectedExceptionExceptionAction(ex));
        }

        dispatcher.Dispatch(new BookDeletedAction(action.BookId));
    }

}