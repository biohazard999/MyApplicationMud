using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using MudBlazor.Services;

using MyApplicationMud;
using MyApplicationMud.Services;
using MyApplicationMud.GraphQL;
using StrawberryShake;
using Fluxor;
using MyApplicationMud.Store;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddFluxor(configuration =>
{
    configuration.ScanAssemblies(typeof(CounterState).Assembly);

#if DEBUG
    configuration.UseReduxDevTools(devtools =>
    {
        devtools.EnableStackTrace();
    });
#endif
});

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// authentication state plumbing
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, BffAuthenticationStateProvider>();

// HTTP client configuration
builder.Services.AddTransient<AntiforgeryHandler>();

builder.Services
    .AddHttpClient("backend", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<AntiforgeryHandler>();

builder.Services
    .AddMyApplicationMudClient(ExecutionStrategy.CacheFirst)
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri($"{builder.HostEnvironment.BaseAddress}external-graphql");
    })
    .ConfigureWebSocketClient(client =>
    {
        var uri = new Uri(builder.HostEnvironment.BaseAddress);
        var websocketUri = $"wss://{uri.Authority}/ws-external-graphql";
        client.Uri = new Uri(websocketUri);
        client.ConnectionInterceptor = new AntiforgeryWebsocketConnectionInterceptor();
    });

builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("backend"));

builder.Services.AddMudServices();
builder.Services.AddMudMarkdownServices();

await builder.Build().RunAsync();
