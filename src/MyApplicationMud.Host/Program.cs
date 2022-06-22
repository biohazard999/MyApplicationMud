using Duende.Bff.Yarp;

using Yarp.ReverseProxy.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddBff();

var reverse = builder.Services
    .AddReverseProxy()
    .AddTransforms<AccessTokenTransformProvider>()
    .AddBffExtensions()
    //.LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .LoadFromMemory(
    new[]
    {
        new RouteConfig()
        {
            RouteId = "external-api",
            ClusterId = "external-api",

            Match = new()
            {
                Path = "/external-api/{**catch-all}"
            },
            Transforms = new List<Dictionary<string, string>>()
            {
                new()
                {
                    ["PathRemovePrefix"] = "/external-api"
                }
            }
        }.WithAccessToken(Duende.Bff.TokenType.User)
    },
    new[]
    {
        new ClusterConfig
        {
            ClusterId = "external-api",

            Destinations = new Dictionary<string, DestinationConfig>(StringComparer.OrdinalIgnoreCase)
            {
                {
                    "external-api", new()
                    {
                        Address = "https://localhost:7222/api/"
                    }
                },
            }
        }
    })
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseBff();
app.UseAuthorization();

app.MapBffManagementEndpoints();

app.MapBffReverseProxy(false);

app.MapRazorPages();

app.MapControllers()
    .RequireAuthorization()
    .AsBffApiEndpoint();

app.MapFallbackToFile("index.html");

app.Run();
