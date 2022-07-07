using System.Globalization;

using Blazored.LocalStorage;

using FluentValidation;

using Fluxor.Persist.Middleware;
using Fluxor.Persist.Storage;

using Microsoft.AspNetCore.Components.Authorization;

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using MudBlazor.Services;

using MyApplicationMud;
using MyApplicationMud.Services;
using MyApplicationMud.Services.Storage;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddLocalization();
builder.Services.AddBlazoredLocalStorage(config => config.JsonSerializerOptions.WriteIndented = true);
builder.Services.AddScoped<IStringStateStorage, LocalStateStorage>();
builder.Services.AddScoped<IStoreHandler, JsonStoreHandler>();

builder.Services.AddFluxor(configuration =>
{
    configuration.ScanAssemblies(typeof(CounterState).Assembly);
    configuration.UseRouting();
    configuration.UsePersist(o => o.UseInclusionApproach());

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

var host = builder.Build();

CultureInfo cultureInfo = new("de-AT");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
CultureInfo.CurrentUICulture = cultureInfo;
CultureInfo.CurrentCulture = cultureInfo;
ValidatorOptions.Global.LanguageManager.Enabled = true;
ValidatorOptions.Global.LanguageManager.Culture = cultureInfo;

await host.RunAsync();
