using Microsoft.AspNetCore.Components.Authorization;

namespace MyApplicationMud.Stores;


public record UserLoggedIn(User User);

public record UserLoggedOut();


[PersistState]
public record LoginState
{
    public User User { get; init; } = User.Empty;

    [JsonIgnore]
    public bool IsAuthenticated => User.IsAuthenticated;
}

public class LoginStateFeature : Feature<LoginState>
{
    public override string GetName() => nameof(LoginState);

    protected override LoginState GetInitialState()
        => new();
}

public record User(string UserName, bool IsAuthenticated, List<ClaimRecord> Claims)
{
    public static readonly User Empty = new("", false, new());

    public static User FromClaimsPrincipal(ClaimsPrincipal principal)
    {
        var name = principal.Identity?.Name ?? "";
        var isAuthenticated = principal.Identity?.IsAuthenticated ?? false;
        var claims = principal.Claims.Select(c => new ClaimRecord(c.Type, c.Value)).ToList();
        return new User(name, isAuthenticated, claims);
    }

    public ClaimsPrincipal ToClaimsPrincipal()
    {
        if (this == Empty)
        {
            return new ClaimsPrincipal(new ClaimsIdentity());
        }

        var identity = new ClaimsIdentity(
            nameof(AuthenticationStateProvider),
            "name",
            "role"
        );

        foreach (var claim in Claims ?? Enumerable.Empty<ClaimRecord>())
        {
            identity.AddClaim(new Claim(claim.Type, claim.Value?.ToString() ?? ""));
        }

        return new ClaimsPrincipal(identity);
    }
}

public record ClaimRecord(string Type, object Value);

public static class LoginStateReducers
{
    [ReducerMethod]
    public static LoginState UserLoggedIn(LoginState appState, UserLoggedIn action)
       => appState with
       {
           User = action.User
       };

    [ReducerMethod(typeof(UserLoggedOut))]
    public static LoginState UserLoggedOut(LoginState appState)
       => appState with
       {
           User = User.Empty
       };
}
