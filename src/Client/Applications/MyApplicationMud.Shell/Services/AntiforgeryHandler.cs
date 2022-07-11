
using StrawberryShake.Transport.WebSockets;

namespace MyApplicationMud.Services;

public class AntiforgeryHandler : DelegatingHandler
{
    public AntiforgeryHandler()
    {

    }

    public AntiforgeryHandler(HttpMessageHandler delegatingHandler) : base(delegatingHandler)
    {

    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Add("X-CSRF", "1");
        return base.SendAsync(request, cancellationToken);
    }
}

public class AntiforgeryWebsocketConnectionInterceptor
    : ISocketConnectionInterceptor
{
    // the object returned by this method, will be included in the connection initialization message
    public ValueTask<object?> CreateConnectionInitPayload(
        ISocketProtocol protocol,
        CancellationToken cancellationToken)
    {
        return new ValueTask<object?>(
            new Dictionary<string, string>
            {
                ["X-CSRF"] = "1"
            }
        );
    }
}
