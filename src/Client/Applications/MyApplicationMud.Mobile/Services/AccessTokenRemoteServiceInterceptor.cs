using System.IdentityModel.Tokens.Jwt;

using IdentityModel.Client;
using IdentityModel.OidcClient;

namespace MyApplicationMud.Services;

public class AccessTokenHandler : AntiforgeryHandler
{
    public OidcClient OidcClient { get; }

    public AccessTokenHandler(OidcClient oidcClient)
        => OidcClient = oidcClient;

    public AccessTokenHandler(OidcClient oidcClient, HttpMessageHandler handler) : base(handler)
        => OidcClient = oidcClient;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var currentAccessToken = await SecureStorage.GetAsync(OidcConsts.AccessTokenKeyName);

        if (!string.IsNullOrEmpty(currentAccessToken))
        {
            // TODO: Find better way to find if token is expired instead of parsing it.
            var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(currentAccessToken);
            if (jwtToken.ValidTo <= DateTime.UtcNow)
            {
                var refreshToken = await SecureStorage.GetAsync(OidcConsts.RefreshTokenKeyName);
                if (!string.IsNullOrEmpty(refreshToken))
                {
                    var refreshResult = await OidcClient.RefreshTokenAsync(refreshToken);

                    await SecureStorage.SetAsync(OidcConsts.AccessTokenKeyName, refreshResult.AccessToken);
                    await SecureStorage.SetAsync(OidcConsts.RefreshTokenKeyName, refreshResult.RefreshToken);

                    request.SetBearerToken(refreshResult.AccessToken);
                }
                else
                {
                    var loginResult = await OidcClient.LoginAsync(new LoginRequest());

                    await SecureStorage.SetAsync(OidcConsts.AccessTokenKeyName, loginResult.AccessToken);
                    await SecureStorage.SetAsync(OidcConsts.RefreshTokenKeyName, loginResult.RefreshToken);

                    request.SetBearerToken(loginResult.AccessToken);
                }
            }

            request.SetBearerToken(currentAccessToken);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}

