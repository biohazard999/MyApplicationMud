using System.Reactive.Linq;

namespace MyApplicationMud.Store;

public record BooksState
{
    public bool IsLoading { get; init; }
    public bool HasErrors => Errors.Any();
    public IList<IGetBooksListView_Items> Items { get; init; } = new List<IGetBooksListView_Items>();
    public IEnumerable<IClientError> Errors { get; init; } = Enumerable.Empty<IClientError>();
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

public record RefreshBooksAction();

public record BooksLoadedAction(IEnumerable<IGetBooksListView_Items> Items);

public record BooksLoadedWithClientErrorsAction(IEnumerable<IClientError> Errors);

public class RefreshBooksEffect : Effect<RefreshBooksAction>, IDisposable
{
    IDisposable? Session;
    public IMyApplicationMudClient Client { get; }
    public ISnackbar Snackbar { get; set; }

    public RefreshBooksEffect(IMyApplicationMudClient Client, ISnackbar Snackbar)
        => (this.Client, this.Snackbar) = (Client, Snackbar);

    public override Task HandleAsync(RefreshBooksAction action, IDispatcher dispatcher)
    {
        Session?.Dispose();

        Session = Client
           .GetBooksListView
           .Watch(ExecutionStrategy.CacheFirst)
           .Catch((Exception e) =>
           {
               dispatcher.Dispatch(new UnexpectedExceptionExceptionAction(e));
               return Observable.Empty<IOperationResult<IGetBooksListViewResult>>();
           })
           .Subscribe(result =>
           {
               dispatcher.Dispatch(new BooksLoadedWithClientErrorsAction(result.Errors));
               if (result.Data is not null)
               {
                   dispatcher.Dispatch(new BooksLoadedAction(result.Data.Items));
                   Snackbar.Add("Daten wurden aktualisiert", Severity.Success);
               }
           });

        return Task.CompletedTask;
    }

    public void Dispose()
        => Session?.Dispose();
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
}