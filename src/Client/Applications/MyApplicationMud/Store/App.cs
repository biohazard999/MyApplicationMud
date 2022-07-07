using Microsoft.AspNetCore.Components;
using MyApplicationMud.Shared;

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

public record UnexpectedExceptionAction(Exception Exception);
public record ClearUnexpectedExceptionAction();

public static class AppReducer
{
    [ReducerMethod]
    public static AppState SetExceptionReducer(AppState appState, UnexpectedExceptionAction exceptionAction)
        => appState with
        {
            Exception = exceptionAction.Exception
        };

    [ReducerMethod(typeof(ClearUnexpectedExceptionAction))]
    public static AppState ClearUnexpectedExceptionAction(AppState appState)
        => appState with
        {
            Exception = null
        };
}

public record AppEffects(IDialogService DialogService)
{
    [EffectMethod]
    public async Task SetExceptionEffect(UnexpectedExceptionAction action, IDispatcher dispatcher)
    {
        var result = DialogService.Show<ExceptionDetails>("Unerwarteter Fehler", new DialogParameters
        {
            [nameof(ExceptionDetails.Exception)] = action.Exception
        }, new DialogOptions
        {
            CloseOnEscapeKey = true,
            Position = DialogPosition.TopCenter,
            MaxWidth = MaxWidth.Medium,
            FullWidth = true
        });

        _ = await result.GetReturnValueAsync<object?>();

        dispatcher.Dispatch(new ClearUnexpectedExceptionAction());
    }
}