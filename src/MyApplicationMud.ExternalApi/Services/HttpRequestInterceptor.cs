using HotChocolate.AspNetCore;
using HotChocolate.Execution;
using System.Security.Claims;

namespace MyApplicationMud.ExternalApi.Services;

public class HttpRequestInterceptor : DefaultHttpRequestInterceptor
{
    public override ValueTask OnCreateAsync(HttpContext context,
        IRequestExecutor requestExecutor, IQueryRequestBuilder requestBuilder,
        CancellationToken cancellationToken)
    {
        var identity = new ClaimsIdentity(
            context.User.Claims, 
            context.User.Identity?.AuthenticationType, 
            "name", 
            "role"
        );

        context.User = new ClaimsPrincipal();
        context.User.AddIdentity(identity);

        return base.OnCreateAsync(context, requestExecutor, requestBuilder,
            cancellationToken);
    }
}