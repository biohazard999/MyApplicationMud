using MyApplicationMud.Components;
using MyApplicationMud.GraphQL;
using MyApplicationMud.Shared;
using MyApplicationMud.Shared.Validation;

namespace MyApplicationMud.Stores;

[Dispatchable]
public record BooksLoadingAction();
[Dispatchable]
public record BookChangedAction(IBookListInfo Book);
[Dispatchable]
public record BookAddedAction(IBookListInfo Book);
[Dispatchable]
public record BookDeletedAction(int BookId);
[Dispatchable]
public record RefreshBooksAction();
[Dispatchable]
public record BooksLoadedAction(IEnumerable<IBookListInfo> Items);
[Dispatchable]
public record BooksLoadedWithClientErrorsAction(IEnumerable<IClientError> Errors);
[Dispatchable]
public record SaveBookAction(int? BookId, BookModelInput BookModel, Stream? PictureStream = null);
[Dispatchable]
public record DeleteBookAction(int BookId);
[Dispatchable]
public record AddBookAction();
[Dispatchable]
public record EditBookAction(int BookId);
[Dispatchable]
public record EditBookFetchedAction(int BookId, BookModelInput BookModel, string? BookImage = null);
[Dispatchable]
public record AuthorsFetchedAction(IEnumerable<IAuthorInfo> Authors);
[Dispatchable]
public record ShowDialogAction(DialogReference DialogReference);
[Dispatchable]
public record CloseDialogAction(DialogReference DialogReference);
[Dispatchable]
public record RemoveDialogAction();
[Dispatchable]
public record SetBookImageAction(string? BookImage);

[PersistState]
public record BooksState
{
    [JsonIgnore]
    public bool IsLoading { get; init; }
    [JsonIgnore]
    public bool HasErrors => Errors.Any();
    public IList<IBookListInfo> Items { get; init; } = new List<IBookListInfo>();
    public IEnumerable<IClientError> Errors { get; init; } = Enumerable.Empty<IClientError>();

    public int? BookId { get; init; }

    public string? BookImage { get; init; }
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

public class RefreshBooksEffect : Effect<RefreshBooksAction>, IDisposable
{
    private IDisposable? Subscription { get; set; }
    public IMyApplicationMudBooksClient Client { get; }
    public ISnackbar Snackbar { get; set; }

    public RefreshBooksEffect(IMyApplicationMudBooksClient client, ISnackbar snackbar)
        => (Client, Snackbar) = (client, snackbar);

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
    public static BooksState AuthorsFetchedReducer(BooksState state, AuthorsFetchedAction action)
        => state with { Authors = action.Authors };

    [ReducerMethod]
    public static BooksState EditBookFetchedReducer(BooksState state, EditBookFetchedAction action)
        => state with { BookId = action.BookId, EditBook = action.BookModel, BookImage = action.BookImage };

    [ReducerMethod]
    public static BooksState ShowDialogReducer(BooksState state, ShowDialogAction action)
        => state with { CurrentDialog = action.DialogReference };

    [ReducerMethod(typeof(RemoveDialogAction))]
    public static BooksState RemoveDialogReducer(BooksState state)
        => state with { CurrentDialog = null };

    [ReducerMethod(typeof(CloseDialogAction))]
    public static BooksState ShowDialogReducer(BooksState state)
        => state with { CurrentDialog = null, BookId = null, EditBook = null, BookImage = null };

    [ReducerMethod]
    public static BooksState SetBookImageReducer(BooksState state, SetBookImageAction action)
        => state with { BookImage = action.BookImage };
}

public record BookEffects(
    IMyApplicationMudBooksClient Client,
    IHttpClientFactory HttpClientFactory,
    IDialogService DialogService,
    IState<BooksState> State
)
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
            }, authors.Data.Details.Image));
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
        var validationResult = new BookInputModelValidator().Validate(action.BookModel);
        if (validationResult is not null)
        {
            //validationResult.IsValid
            //validationResult.Errors.ForEach(a => a.)
        }

        int? bookId = action.BookId;

        if (action.BookId.HasValue)
        {
            var result = await Client.UpdateBook.ExecuteAsync(action.BookId.Value.ToString(), action.BookModel);

            result.EnsureNoErrors();

            if (result.Data is not null)
            {
                if (result.Data.EditBook.Errors.Any())
                {
                    dispatcher.DispatchSetValidationErrors(result.Data.EditBook.Errors.Select(m => new KeyValuePair<string, string>(m.PropertyName, m.Message)));
                    return;
                }
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
            if (result.Data is not null)
            {
                bookId = result.Data.AddBook.Id;
            }

            result.EnsureNoErrors();
        }

        //## TODO: Multipart
        //## Waiting for https://github.com/ChilliCream/hotchocolate/issues/3312
        //## Multipart Spec is supported by Server: https://github.com/jaydenseric/graphql-multipart-request-spec
        if (action.PictureStream is not null)
        {
            using var form = new MultipartFormDataContent();

            form.Add(new StringContent("{ \"query\": \"mutation UploadPicture($file: Upload!) { setBookImage(bookId: " + bookId + ", file: $file) { id } }\", \"variables\": { \"file\": null } }"), "operations");
            form.Add(new StringContent("{ \"0\": [\"variables.file\"] }"), "map");
            form.Add(new StreamContent(action.PictureStream), "0", "file.png");

            var client = HttpClientFactory.CreateClient("MyApplicationMudClient");
            var response = await client.PostAsync("", form);
            var result = await response.Content.ReadAsStringAsync();
            //TODO: Result Handling
        }

        dispatcher.Dispatch(new CloseDialogAction(State.Value.CurrentDialog!));
    }

    [EffectMethod]
    public async Task HandleDeleteBookAction(DeleteBookAction action, IDispatcher dispatcher)
    {
        var result = await Client.DeleteBook.ExecuteAsync(action.BookId.ToString());

        result.EnsureNoErrors();

        dispatcher.Dispatch(new BookDeletedAction(action.BookId));
    }
}
