using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;

using Microsoft.AspNetCore.Components.Authorization;

namespace MyApplicationMud.Services;

public class BffAuthenticationStateProvider : AuthenticationStateProvider
{
    private static readonly TimeSpan userCacheRefreshInterval = TimeSpan.FromSeconds(60);

    private readonly HttpClient client;
    private readonly ILogger<BffAuthenticationStateProvider> logger;

    private DateTimeOffset userLastCheck = DateTimeOffset.FromUnixTimeSeconds(0);
    private ClaimsPrincipal cachedUser = new(new ClaimsIdentity());

    public BffAuthenticationStateProvider(
        HttpClient client,
        ILogger<BffAuthenticationStateProvider> logger)
    {
        this.client = client;
        this.logger = logger;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = await GetUser();
        var state = new AuthenticationState(user);

        // checks periodically for a session state change and fires event
        // this causes a round trip to the server
        // adjust the period accordingly if that feature is needed
        if (user.Identity?.IsAuthenticated == true)
        {
            logger.LogInformation("starting background check..");
            Timer? timer = null;

            timer = new Timer(async _ =>
            {
                var currentUser = await GetUser(false);
                if (currentUser.Identity?.IsAuthenticated == false)
                {
                    logger.LogInformation("user logged out");
                    NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(currentUser)));
                    if (timer is not null)
                    {
                        await timer.DisposeAsync();
                    }
                }
            }, null, 1000, 5000);
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
                    nameof(BffAuthenticationStateProvider),
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
