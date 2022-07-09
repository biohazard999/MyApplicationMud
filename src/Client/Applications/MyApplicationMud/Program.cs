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
using MyApplicationMud.Stores;
using MyApplicationMud.Services;
using MyApplicationMud.Services.Storage;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<Main>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddMyApplicationMud<
        AntiforgeryHandler
    >(builder.HostEnvironment.BaseAddress);

var host = builder.Build();

CultureInfo cultureInfo = new("de-AT");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
CultureInfo.CurrentUICulture = cultureInfo;
CultureInfo.CurrentCulture = cultureInfo;
ValidatorOptions.Global.LanguageManager.Enabled = true;
ValidatorOptions.Global.LanguageManager.Culture = cultureInfo;

await host.RunAsync();
