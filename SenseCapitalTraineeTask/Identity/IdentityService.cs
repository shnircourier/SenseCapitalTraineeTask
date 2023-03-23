using IdentityModel.Client;
using SC.Internship.Common.Exceptions;

namespace SenseCapitalTraineeTask.Identity;

public class IdentityService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public IdentityService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task<HttpClient> GetAuthorizedClient()
    {
        var client = _httpClientFactory.CreateClient();
        var discovery = await GetDiscoveryDocument();
        
        var response = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = discovery?.TokenEndpoint,
            ClientId = "client_id",
            ClientSecret = "client_secret",
            Scope = "MyApi"
        });
        
        client.SetBearerToken(response.AccessToken);

        return client;
    }

    private async Task<DiscoveryDocumentResponse?> GetDiscoveryDocument()
    {
        var client = _httpClientFactory.CreateClient();
        
        var discovery = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
        {
            Address = _configuration["Auth:Authority"],
            Policy =
            {
                RequireHttps = false
            }
        });
        
        if (discovery.IsError)
        {
            throw new ScException($"Сервис авторизации по адресу {_configuration["Auth:Authority"]} временно недоступен");
        }

        return discovery;
    }
}