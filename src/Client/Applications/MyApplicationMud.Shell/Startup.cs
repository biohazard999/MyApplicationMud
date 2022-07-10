using Blazored.LocalStorage;

using Fluxor.Persist.Middleware;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;

using MudBlazor.Services;

using MyApplicationMud.Services;
using MyApplicationMud.Services.Storage;

namespace MyApplicationMud;

public static class Startup
{
    public static void AddMyApplicationMud<
        TDelegatingHandler,
        TAuthenticationStateProvider
    >
    (this IServiceCollection services, string baseUri)
        where TDelegatingHandler : DelegatingHandler
        where TAuthenticationStateProvider : AuthenticationStateProvider
    {
        services.AddLocalization();
        services.AddBlazoredLocalStorage(config => config.JsonSerializerOptions.WriteIndented = true);
        services.AddScoped<IStringStateStorage, LocalStateStorage>();
        services.AddScoped<IStoreHandler, JsonStoreHandler>();

        services.AddFluxor(configuration =>
        {
            configuration.ScanAssemblies(
                typeof(AppState).Assembly,
                typeof(CounterState).Assembly,
                typeof(BooksState).Assembly,
                typeof(ValidationState).Assembly,
                typeof(WeatherState).Assembly
            );

            configuration.UseRouting();
            configuration.UsePersist(o => o.UseInclusionApproach());

#if DEBUG
            configuration.UseReduxDevTools(devtools =>
            {
                devtools.EnableStackTrace();
            });
#endif

        });

        // authentication state plumbing
        services.AddAuthorizationCore();
        services.AddScoped<AuthenticationStateProvider, TAuthenticationStateProvider>();

        services.TryAddTransient<IRedirectToLoginHandler, RedirectToLoginHandler>();

        // HTTP client configuration
        services
            .AddTransient<TDelegatingHandler>();

        services
            .AddHttpClient("backend", client => client.BaseAddress = new Uri(baseUri))
            .AddHttpMessageHandler<TDelegatingHandler>();

        services
            .AddMyApplicationMudBooksClient(ExecutionStrategy.CacheFirst)
            .ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri($"{new Uri(baseUri)}external-graphql");
            })
            .ConfigureWebSocketClient(client =>
            {
                var uri = new Uri(baseUri);
                var websocketUri = $"wss://{uri.Authority}/ws-external-graphql";
                client.Uri = new Uri(websocketUri);
                client.ConnectionInterceptor = new AntiforgeryWebsocketConnectionInterceptor();
            });

        services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("backend"));

        services.AddMudServices();
        services.AddMudMarkdownServices();
    }
}
