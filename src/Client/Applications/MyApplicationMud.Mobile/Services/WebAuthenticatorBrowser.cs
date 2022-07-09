using System.Diagnostics;

using IdentityModel.OidcClient.Browser;

using IBrowser = IdentityModel.OidcClient.Browser.IBrowser;

namespace MyApplicationMud.Services;

internal class WebAuthenticatorBrowser : IBrowser
{
    public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
    {
        try
        {
#if WINDOWS
            var authResult =
                await WinUIEx.WebAuthenticatorWin.AuthenticateAsync(new Uri(options.StartUrl), new Uri(options.EndUrl));
#else

            var authResult =
                await WebAuthenticator.AuthenticateAsync(new Uri(options.StartUrl), new Uri(options.EndUrl));
#endif
            var authorizeResponse = ToRawIdentityUrl(options.EndUrl, authResult);

            return new BrowserResult
            {
                Response = authorizeResponse
            };
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return new BrowserResult()
            {
                ResultType = BrowserResultType.UnknownError,
                Error = ex.ToString()
            };
        }
    }

#if WINDOWS //https://github.com/dotnet/maui/issues/2702
    public string ToRawIdentityUrl(string redirectUrl, WebAuthenticatorResult result)
    {
        try
        {
            IEnumerable<string> parameters = result.Properties.Select(pair => $"{pair.Key}={pair.Value}");
            var modifiedParameters = parameters.ToList();

            var stateParameter = modifiedParameters
                .FirstOrDefault(p => p.StartsWith("state", StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(stateParameter))
            {
                // Remove the state key added by WebAuthenticator that includes appInstanceId
                modifiedParameters = modifiedParameters.Where(p => !p.StartsWith("state", StringComparison.OrdinalIgnoreCase)).ToList();

                stateParameter = System.Web.HttpUtility.UrlDecode(stateParameter).Split('&').Last();
                modifiedParameters.Add(stateParameter);
            }
            var values = string.Join("&", modifiedParameters);
            return $"{redirectUrl}#{values}";
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            throw;
        }
    }
#else
    public string ToRawIdentityUrl(string redirectUrl, WebAuthenticatorResult result)
    {
        var parameters = result.Properties.Select(pair => $"{pair.Key}={pair.Value}");
        var values = string.Join("&", parameters);

        return $"{redirectUrl}#{values}";
    }
#endif
}
