using System;
using System.Linq;

using IdentityModel.OidcClient;

namespace MyApplicationMud.Services;

public record MauiRedirectToLoginHandler(
    OidcClient OidcClient,
    IDispatcher Dispatcher
) : IRedirectToLoginHandler
{
    public Task Redirect(string? returnUrl = null) => Task.CompletedTask;
}
