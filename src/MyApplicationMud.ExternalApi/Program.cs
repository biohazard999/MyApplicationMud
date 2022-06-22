using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging(l => l.AddConsole());

var services = builder.Services;
var configuration = builder.Configuration;
var env = builder.Environment;


services.AddControllers();
services.AddAuthorization();
services
    .AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        configuration.GetSection("OpenIDConnectSettings").Bind(options);
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });

var app = builder.Build();

app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
