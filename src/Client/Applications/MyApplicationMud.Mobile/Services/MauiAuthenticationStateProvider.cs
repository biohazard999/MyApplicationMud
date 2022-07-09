using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;

using IdentityModel.OidcClient;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;

namespace MyApplicationMud.Services;

public class MauiAuthenticationStateProvider : AuthenticationStateProvider
{
    private static readonly TimeSpan userCacheRefreshInterval = TimeSpan.FromSeconds(60);
    private readonly OidcClient oidcClient;
    private readonly HttpClient client;
    private readonly ILogger<MauiAuthenticationStateProvider> logger;

    private DateTimeOffset userLastCheck = DateTimeOffset.FromUnixTimeSeconds(0);
    private ClaimsPrincipal cachedUser = new(new ClaimsIdentity());

    public MauiAuthenticationStateProvider(
        OidcClient oidcClient,
        HttpClient client,
        ILogger<MauiAuthenticationStateProvider> logger)
    {
        this.oidcClient = oidcClient;
        this.client = client;
        this.logger = logger;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var result = await oidcClient.LoginAsync(new LoginRequest());
        if (result.IsError)
        {
            return new AuthenticationState(new(new ClaimsIdentity()));
        }
        var user = result.User ?? new(new ClaimsIdentity());
        var state = new AuthenticationState(user);

        // checks periodically for a session state change and fires event
        // this causes a round trip to the server
        // adjust the period accordingly if that feature is needed
        if (user.Identity?.IsAuthenticated == true)
        {
            await SecureStorage.SetAsync(OidcConsts.AccessTokenKeyName, result.AccessToken);
            await SecureStorage.SetAsync(OidcConsts.RefreshTokenKeyName, result.RefreshToken);
        }

        return state;
    }

    private async ValueTask<ClaimsPrincipal> GetUser(bool useCache = true)
    {
        var now = DateTimeOffset.Now;
        if (useCache && now < userLastCheck + userCacheRefreshInterval)
        {
            logger.LogDebug("Taking user from cache");
            return cachedUser;
        }

        logger.LogDebug("Fetching user");
        cachedUser = await FetchUser();
        userLastCheck = now;

        return cachedUser;
    }

    record ClaimRecord(string Type, object Value);

    private async Task<ClaimsPrincipal> FetchUser()
    {
        try
        {
            logger.LogInformation("Fetching user information.");
            var response = await client.GetAsync("bff/user?slide=false");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var claims = await response.Content.ReadFromJsonAsync<List<ClaimRecord>>();

                var identity = new ClaimsIdentity(
                    nameof(MauiAuthenticationStateProvider),
                    "name",
                    "role");

                foreach (var claim in claims)
                {
                    identity.AddClaim(new Claim(claim.Type, claim.Value.ToString()));
                }

                return new ClaimsPrincipal(identity);
            }
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Fetching user failed.");
        }

        return new ClaimsPrincipal(new ClaimsIdentity());
    }
}
