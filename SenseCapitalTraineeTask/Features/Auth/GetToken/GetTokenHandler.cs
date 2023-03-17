using IdentityModel.Client;
using MediatR;

namespace SenseCapitalTraineeTask.Features.Auth.GetToken;

public class GetTokenHandler : IRequestHandler<GetTokenQuery, string>
{
    private readonly IHttpClientFactory _httpClientFactory;

    public GetTokenHandler(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task<string> Handle(GetTokenQuery request, CancellationToken cancellationToken)
    {
        var authClient = _httpClientFactory.CreateClient();

        var discovery = await authClient.GetDiscoveryDocumentAsync("https://localhost:44371", cancellationToken: cancellationToken);

        var response = await authClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = discovery.TokenEndpoint,
            ClientId = "client_id",
            ClientSecret = "client_secret",
            Scope = "MyApi"
        }, cancellationToken: cancellationToken);

        return response.AccessToken;
    }
}