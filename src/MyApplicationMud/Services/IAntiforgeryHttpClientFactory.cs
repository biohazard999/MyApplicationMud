using MyApplicationMud.Shared.Defaults;

namespace MyApplicationMud.Services;

public interface IAntiforgeryHttpClientFactory
{
    Task<HttpClient> CreateClientAsync(string clientName = AuthDefaults.AuthorizedClientName);
}
