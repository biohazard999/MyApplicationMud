using Fluxor;

namespace MyApplicationMud.Store;

public record CounterState
{
    public int Count { get; init; }
}

public record IncrementCounterAction();

public class CounterFeature : Feature<CounterState>
{
    public override string GetName()
         => nameof(CounterState);

    protected override CounterState GetInitialState()
        => new CounterState { Count = 0 };
}

public static class CounterReducer
{
    [ReducerMethod(typeof(IncrementCounterAction))]
    public static CounterState ReduceCounterAction(CounterState counterState)
        => counterState with
        {
            Count = counterState.Count + 1
        };
}