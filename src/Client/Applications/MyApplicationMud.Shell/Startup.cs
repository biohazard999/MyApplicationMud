using Blazored.LocalStorage;

using Fluxor.Persist.Middleware;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection.Extensions;

using MudBlazor.Services;

using MyApplicationMud.GraphQL;
using MyApplicationMud.Services;
using MyApplicationMud.Services.Storage;

namespace MyApplicationMud;

public record MyApplicationMudConfiguration(Func<IServiceProvider, string> BaseUri)
{
    public bool UseReduxDevTools { get; init; } = true;

    public Func<IServiceProvider, HttpMessageHandler> BackendDelegatingHandler { get; init; } = _ => new EmptyDelegatingHandler();
    public Func<IServiceProvider, HttpMessageHandler> BackendPrimaryHandler { get; init; } = _ => new HttpClientHandler();
    public Func<IServiceProvider, HttpMessageHandler> GraphQLDelegatingHandler { get; init; } = _ => new EmptyDelegatingHandler();
    public Func<IServiceProvider, HttpMessageHandler> GraphQLPrimaryHandler { get; init; } = _ => new HttpClientHandler();

    public Func<IServiceProvider, AuthenticationStateProvider> AuthenticationStateProvider { get; init; } = _ => new EmptyAuthenticationStateProvider();

    internal class EmptyDelegatingHandler : DelegatingHandler { }

    internal class EmptyAuthenticationStateProvider : AuthenticationStateProvider
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync() => Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
    }
}


public class DelegatingHandlerProxy : DelegatingHandler
{
    public DelegatingHandlerProxy() { }

    public DelegatingHandlerProxy(HttpMessageHandler innerHandler) : base(innerHandler)
    {
    }
}

public static class Startup
{
    public static void AddMyApplicationMud(
        this IServiceCollection services, MyApplicationMudConfiguration appConfiguration)
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

            if (appConfiguration.UseReduxDevTools)
            {
                configuration.UseReduxDevTools(devtools =>
                {
                    devtools.EnableStackTrace();
                });
            }
        });

        // authentication state plumbing
        services.AddAuthorizationCore();
        services.AddScoped(s => appConfiguration.AuthenticationStateProvider(s));

        services.TryAddTransient<IRedirectToLoginHandler, RedirectToLoginHandler>();

        // HTTP client configuration
        services
            .AddHttpClient("backend", (sp, client) => client.BaseAddress = new Uri(appConfiguration.BaseUri(sp)))
            .ConfigurePrimaryHttpMessageHandler((sp) => appConfiguration.BackendPrimaryHandler(sp))
            .AddHttpMessageHandler(sp =>
            {
                var handler = appConfiguration.BackendDelegatingHandler(sp);
                if (handler is DelegatingHandler delegatingHandler)
                {
                    return delegatingHandler;
                }
                return new DelegatingHandlerProxy(handler);
            });

        services
            .AddHttpClient(nameof(MyApplicationMudBooksClient), (sp, client) => client.BaseAddress = new Uri($"{new Uri(appConfiguration.BaseUri(sp))}external-graphql"))
            .ConfigurePrimaryHttpMessageHandler((sp) => appConfiguration.GraphQLPrimaryHandler(sp))
            .AddHttpMessageHandler(sp =>
            {
                var handler = appConfiguration.GraphQLDelegatingHandler(sp);
                if (handler is DelegatingHandler delegatingHandler)
                {
                    return delegatingHandler;
                }
                return new DelegatingHandlerProxy(handler);
            });

        services
            .AddMyApplicationMudBooksClient(ExecutionStrategy.CacheFirst)
            .ConfigureWebSocketClient((sp, client) =>
            {
                var uri = new Uri(appConfiguration.BaseUri(sp));
                var websocketUri = $"wss://{uri.Authority}/ws-external-graphql";
                client.Uri = new Uri(websocketUri);
                client.ConnectionInterceptor = new AntiforgeryWebsocketConnectionInterceptor();
            });

        services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("backend"));

        services.AddMudServices();
        services.AddMudMarkdownServices();
    }
}
