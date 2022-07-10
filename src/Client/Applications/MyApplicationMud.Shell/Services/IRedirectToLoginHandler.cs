using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;

namespace MyApplicationMud.Services;

public interface IRedirectToLoginHandler
{
    Task Redirect(string? returnUrl = null);
}

public record RedirectToLoginHandler(NavigationManager Navigation) : IRedirectToLoginHandler
{
    public Task Redirect(string? returnUrl = null)
    {
        Navigation.NavigateTo($"bff/login?returnUrl={returnUrl}", forceLoad: true);
        return Task.CompletedTask;
    }
}
