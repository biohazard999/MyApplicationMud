using System;
using System.Linq;

using Microsoft.AspNetCore.Components.Authorization;

namespace MyApplicationMud.Services;

public record ServerRedirectToLoginHandler(
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

        Dispatcher.DispatchUserLoggedOut();
        return;
    }
}
