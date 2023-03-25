using IdentityModel.Client;
using SC.Internship.Common.Exceptions;

namespace SenseCapitalTraineeTask.Identity;

/// <summary>
/// Сервис получения авторизованного клиента
/// </summary>
public class IdentityService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly ILogger<IdentityService> _logger;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="httpClientFactory"></param>
    /// <param name="configuration"></param>
    /// <param name="logger"></param>
    public IdentityService(
        IHttpClientFactory httpClientFactory,
        ILogger<IdentityService> logger,
        IConfiguration configuration
        )
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _logger = logger;
    }

    /// <summary>
    /// Получение авторизованного клиента
    /// </summary>
    /// <returns></returns>
    public async Task<HttpClient> GetAuthorizedClient()
    {
        var client = _httpClientFactory.CreateClient();
        
        _logger.LogInformation("Обращение к документации identity server");
        
        var discovery = await GetDiscoveryDocument();
        
        _logger.LogInformation("Обращение к identity server для получения JWT");
        
        var response = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = discovery?.TokenEndpoint,
            ClientId = "client_id",
            ClientSecret = "client_secret",
            Scope = "MyApi"
        });
        
        _logger.LogInformation("Ответ: {0}", response);
        
        client.SetBearerToken(response.AccessToken);

        return client;
    }

    private async Task<DiscoveryDocumentResponse?> GetDiscoveryDocument()
    {
        var client = _httpClientFactory.CreateClient();
        
        var discovery = await client.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
        {
            Address = Environment.GetEnvironmentVariable("ASPNETCORE_IDENTITY_URL") ?? _configuration["Auth:Authority"],
            Policy =
            {
                RequireHttps = false
            }
        });
        
        if (discovery.IsError)
        {
            throw new ScException($"Сервис авторизации по адресу {Environment.GetEnvironmentVariable("ASPNETCORE_IDENTITY_URL") ?? _configuration["Auth:Authority"]} временно недоступен");
        }

        return discovery;
    }
}