﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using MyApplicationMud.ExternalApi.Queries;
using MyApplicationMud.ExternalApi.Services;

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

services.AddInMemorySubscriptions();

services
    .AddGraphQLServer()
    .AddDefaultTransactionScopeHandler()
    .AddHttpRequestInterceptor<HttpRequestInterceptor>()
    .AddAuthorization()
    .AddInMemorySubscriptions()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .AddQueryType<Query>()
    .AddType<UploadType>()
    .AddMutationType<Mutations>()
    .AddSubscriptionType<Subscriptions>()
//TODO: Relay
//.AddGlobalObjectIdentification()
//.AddQueryFieldToMutationPayloads()

;

var app = builder.Build();

app.UseWebSockets();
app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

    endpoints.MapGraphQL();
});

app.Run();
