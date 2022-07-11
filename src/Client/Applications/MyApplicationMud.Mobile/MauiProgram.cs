using IdentityModel.OidcClient;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

using MyApplicationMud.Services;

using System.Reflection;

namespace MyApplicationMud;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        ConfigureConfiguration(builder);

        builder.Services.AddMauiBlazorWebView();
#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
#endif

        builder.Services
            .AddTransient<IRedirectToLoginHandler, MauiRedirectToLoginHandler>();

        builder.Services
            .AddTransient<WebAuthenticatorBrowser>();

        builder.Services
            .AddTransient(sp =>
            {
#if ANDROID
                var handler = new Platforms.Android.CustomAndroidMessageHandler();
#else
                var handler = new HttpClientHandler();
#endif
                handler.ServerCertificateCustomValidationCallback
                    = (message, cert, chain, errors) => true;

                return new AccessTokenHandler(sp.GetRequiredService<OidcClient>(), handler);
            });

        static HttpClient GetInsecureHttpClient()
        {
#if ANDROID
            var handler = new Platforms.Android.CustomAndroidMessageHandler();
#else
            var handler = new HttpClientHandler();
#endif
            handler.ServerCertificateCustomValidationCallback
                = (message, cert, chain, errors) => true;

            var client = new HttpClient(handler);
            return client;
        }

        builder.Services.AddTransient(sp =>
        {
            var options = new OidcClientOptions();
            var section = builder.Configuration.GetSection("Oidc:Options");
            section.Bind(options);

            options.HttpClientFactory = _ => GetInsecureHttpClient();

            options.Browser = sp.GetRequiredService<WebAuthenticatorBrowser>();
            var client = new OidcClient(options);
            return client;
        });

        var baseUrl = builder.Configuration
            .GetRequiredSection("RemoteServices")
            .GetRequiredSection("Default")
            .GetValue<string>("BaseUrl");

        builder.Services.AddTransient<MauiAuthenticationStateProvider>();
        builder.Services.AddMyApplicationMud(new(_ => baseUrl)
        {
            AuthenticationStateProvider = sp => sp.GetRequiredService<MauiAuthenticationStateProvider>(),
            BackendDelegatingHandler = sp => sp.GetRequiredService<AccessTokenHandler>(),
            GraphQLPrimaryHandler = _ =>
            {
#if ANDROID
                var handler = new Platforms.Android.CustomAndroidMessageHandler();
#else
                var handler = new HttpClientHandler();
#endif
                handler.ServerCertificateCustomValidationCallback
                    = (message, cert, chain, errors) => true;

                return handler;
            }
        });

        return builder.Build();
    }

    private static void ConfigureConfiguration(MauiAppBuilder builder)
    {
        var assembly = typeof(App).GetTypeInfo().Assembly;

        builder.Configuration.AddJsonFile(new EmbeddedFileProvider(assembly, "MyApplicationMud"), "appsettings.json", optional: false, false);
    }
}
