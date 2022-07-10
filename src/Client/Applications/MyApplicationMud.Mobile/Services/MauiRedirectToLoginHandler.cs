using System;
using System.Linq;

using IdentityModel.OidcClient;

using Microsoft.AspNetCore.Components.Authorization;

namespace MyApplicationMud.Services;

public record MauiRedirectToLoginHandler(
    OidcClient OidcClient,
    IState<LoginState> LoginState,
    IDispatcher Dispatcher
) : IRedirectToLoginHandler
{
    public async Task Redirect(string? returnUrl = null)
    {
        if (LoginState.Value.IsAuthenticated)
        {
            return;
        }

        var result = await OidcClient.LoginAsync(new LoginRequest());
        if (result.IsError)
        {
            //TODO: Error handling
            Dispatcher.DispatchUserLoggedOut();
            return;
        }

        var user = User.FromClaimsPrincipal(result.User);

        if (result.User?.Identity?.IsAuthenticated == true)
        {
            await SecureStorage.SetAsync(OidcConsts.AccessTokenKeyName, result.AccessToken);
            await SecureStorage.SetAsync(OidcConsts.RefreshTokenKeyName, result.RefreshToken);
            Dispatcher.DispatchUserLoggedIn(user);
            return;
        }

        Dispatcher.DispatchUserLoggedOut();
        return;
    }
}
