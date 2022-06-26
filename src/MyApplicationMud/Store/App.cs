namespace MyApplicationMud.Store;

public record AppState
{
    public Exception? Exception { get; init; }

    public bool HasError => Exception is not null;
}

public class AppFeature : Feature<AppState>
{
    public override string GetName() => nameof(AppState);

    protected override AppState GetInitialState()
        => new();
}

public record UnexpectedExceptionExceptionAction(Exception Exception);

public static class AppReducer
{
    [ReducerMethod]
    public static AppState SetExceptionReducer(AppState appState, UnexpectedExceptionExceptionAction exceptionAction)
        => appState with
        {
            Exception = exceptionAction.Exception
        };
}