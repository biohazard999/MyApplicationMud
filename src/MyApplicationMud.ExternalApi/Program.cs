var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
var env = builder.Environment;

services.AddAuthentication(options =>
{
    options.DefaultScheme = "Bearer";
})
.AddOpenIdConnect(options =>
{
    configuration.GetSection("OpenIDConnectSettings").Bind(options);

    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.ResponseType = OpenIdConnectResponseType.Code;
    options.Authority = Configuration.GetValue<string>("identityUrl");

    options.ApiName = ApiResources.BotApi.ResourceID;

    options.EnableCaching = true;
    options.CacheDuration = TimeSpan.FromMinutes(10);


    options.SaveTokens = true;
    options.GetClaimsFromUserInfoEndpoint = true;

    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("email");
    options.Scope.Add("xenial");
    options.Scope.Add("address");
    options.Scope.Add("offline_access");
    options.MapInboundClaims = true;

    options.ClaimActions.MapUniqueJsonKey("sub", "sub");
    options.ClaimActions.MapUniqueJsonKey("email", "email");
    options.ClaimActions.MapUniqueJsonKey("name", "name");
    options.ClaimActions.MapUniqueJsonKey("given_name", "given_name");
    options.ClaimActions.MapUniqueJsonKey("family_name", "family_name");
    options.ClaimActions.MapUniqueJsonKey("picture", "picture");
    options.ClaimActions.MapUniqueJsonKey("xenial", "xenial");
    options.ClaimActions.MapUniqueJsonKey("xenial_forecolor", "xenial_forecolor");
    options.ClaimActions.MapUniqueJsonKey("xenial_backcolor", "xenial_backcolor");
    options.ClaimActions.MapUniqueJsonKey("xenial_initials", "xenial_initials");
    options.ClaimActions.MapUniqueJsonKey("address", "address");
    options.ClaimActions.MapUniqueJsonKey("given_name", "given_name");
    options.ClaimActions.MapUniqueJsonKey("family_name", "family_name");
    options.ClaimActions.MapUniqueJsonKey("name", "name");
    options.ClaimActions.MapUniqueJsonKey("company_name", "company_name");
});


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
