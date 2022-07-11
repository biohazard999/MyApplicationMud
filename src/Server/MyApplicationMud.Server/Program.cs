using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using MyApplicationMud.Services;
using MyApplicationMud;
using Duende.Bff.Yarp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBff();

var reverse = builder.Services
    .AddReverseProxy()
    .AddTransforms<AccessTokenTransformProvider>()
    .AddBffExtensions()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
;

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "cookie";
    options.DefaultChallengeScheme = "oidc";
    options.DefaultSignOutScheme = "oidc";
})
    .AddCookie("cookie", options =>
    {
        options.Cookie.Name = "__Host-blazor";
        options.Cookie.SameSite = SameSiteMode.Strict;
    })
    .AddOpenIdConnect("oidc", options =>
    {
        builder.Configuration.GetSection("OpenIDConnectSettings").Bind(options);

        options.ResponseType = "code";
        options.ResponseMode = "query";

        options.Scope.Clear();
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("email");
        options.Scope.Add("xenial");
        options.Scope.Add("address");
        options.Scope.Add("offline_access");
        options.Scope.Add("https://localhost:7222");

        options.MapInboundClaims = false;
        options.GetClaimsFromUserInfoEndpoint = true;
        options.SaveTokens = true;
    });

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services
    .AddMyApplicationMud(new(_ => "https://localhost:7212")
    {
        UseReduxDevTools = false,
        BackendDelegatingHandler = s => s.GetRequiredService<AntiforgeryHandler>(),
        AuthenticationStateProvider = s => s.GetRequiredService<ServerAuthenticationStateProvider>()
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();

app.UseAuthentication();
app.UseBff();
app.UseAuthorization();

app.MapBffManagementEndpoints();

app.MapBffReverseProxy(false);

app.MapRazorPages();

app.MapControllers()
    .RequireAuthorization()
    .AsBffApiEndpoint();

app.MapFallbackToPage("/_Host");

app.Run();
