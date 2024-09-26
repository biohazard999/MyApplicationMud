namespace MyApplicationMud.Stores;

public record ValidationState
{
    public bool HasErrors => !Errors.Any();
    public IList<KeyValuePair<string, string>> Errors { get; init; } = new List<KeyValuePair<string, string>>();
}

public class ValidationStateFeature : Feature<ValidationState>
{
    public override string GetName() => nameof(ValidationState);
    protected override ValidationState GetInitialState()
        => new();
}


public record ClearValidation();

public record SetValidationErrors(IEnumerable<KeyValuePair<string, string>> Errors);

public static class ValidationStateReducers
{
    [ReducerMethod(typeof(ClearValidation))]
    public static ValidationState ClearValidation(ValidationState state)
        => state with { Errors = new List<KeyValuePair<string, string>>() };

    [ReducerMethod]
    public static ValidationState SetValidationErrors(ValidationState state, SetValidationErrors action)
        => state with { Errors = action.Errors.ToList() };
}
