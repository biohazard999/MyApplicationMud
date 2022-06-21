using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyApplicationMud;
using MudBlazor.Services;
using Microsoft.AspNetCore.Components.Authorization;
using MyApplicationMud.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MyApplicationMud.Shared.Defaults;
using System.Net.Http.Headers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var services = builder.Services;

services.AddOptions();
services.AddAuthorizationCore();
services.TryAddSingleton<AuthenticationStateProvider, HostAuthenticationStateProvider>();
services.TryAddSingleton(sp => (HostAuthenticationStateProvider)sp.GetRequiredService<AuthenticationStateProvider>());
services.AddTransient<AuthorizedHandler>();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


services.AddHttpClient("default", client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

services.AddHttpClient(AuthDefaults.AuthorizedClientName, client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
}).AddHttpMessageHandler<AuthorizedHandler>();

services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("default"));
services.AddTransient<IAntiforgeryHttpClientFactory, AntiforgeryHttpClientFactory>();

builder.Services.AddMudServices();

await builder.Build().RunAsync();
