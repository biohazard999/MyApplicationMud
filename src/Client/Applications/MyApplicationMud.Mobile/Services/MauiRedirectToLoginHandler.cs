using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IdentityModel.OidcClient;

namespace MyApplicationMud.Services;

public record MauiRedirectToLoginHandler(OidcClient OidcClient) : IRedirectToLoginHandler
{
    public async Task Redirect()
    {
        var result = await OidcClient.LoginAsync(new LoginRequest());

    }
}
