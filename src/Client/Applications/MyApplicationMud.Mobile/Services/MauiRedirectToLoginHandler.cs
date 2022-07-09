using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IdentityModel.OidcClient;

using Microsoft.AspNetCore.Components.Authorization;

namespace MyApplicationMud.Services;

public record MauiRedirectToLoginHandler : IRedirectToLoginHandler
{
    public Task Redirect() => Task.CompletedTask;
}
