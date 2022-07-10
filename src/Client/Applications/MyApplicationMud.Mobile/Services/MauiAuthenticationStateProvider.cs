using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;

using IdentityModel.OidcClient;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;

namespace MyApplicationMud.Services;

public class MauiAuthenticationStateProvider : AuthenticationStateProvider, IDisposable
{
    private readonly IState<LoginState> loginState;
    private readonly IStore store;

    public MauiAuthenticationStateProvider(IState<LoginState> loginState, IStore store)
    {
        this.loginState = loginState;
        this.store = store;
        store.SubscribeToAction<UserLoggedIn>(this, _ => this.NotifyAuthenticationStateChanged(GetAuthenticationStateAsync()));
        store.SubscribeToAction<UserLoggedOut>(this, _ => this.NotifyAuthenticationStateChanged(GetAuthenticationStateAsync()));
    }

    public void Dispose() => store.UnsubscribeFromAllActions(this);

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (loginState.Value.IsAuthenticated)
        {
            return Task.FromResult(new AuthenticationState(loginState.Value.User.ToClaimsPrincipal()));
        }

        return Task.FromResult(new AuthenticationState(loginState.Value.User.ToClaimsPrincipal()));
    }
}
