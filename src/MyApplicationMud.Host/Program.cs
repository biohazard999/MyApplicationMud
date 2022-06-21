using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

using MyApplicationMud.Shared.Defaults;

using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;
var env = builder.Environment;

services.AddAntiforgery(options =>
{
    options.HeaderName = AntiforgeryDefaults.HeaderName;
    options.Cookie.Name = AntiforgeryDefaults.CookieName;
    options.Cookie.SameSite = SameSiteMode.Strict;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

services.AddHttpClient();
services.AddOptions();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie()
.AddOpenIdConnect(options =>
{
    configuration.GetSection("OpenIDConnectSettings").Bind(options);

    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.ResponseType = OpenIdConnectResponseType.Code;

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

services.AddControllersWithViews(options =>
     options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));

services.AddRazorPages().AddMvcOptions(options =>
{
    //var policy = new AuthorizationPolicyBuilder()
    //    .RequireAuthenticatedUser()
    //    .Build();
    //options.Filters.Add(new AuthorizeFilter(policy));
});

var app = builder.Build();

if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();
app.MapFallbackToPage("/_Host");

app.Run();
