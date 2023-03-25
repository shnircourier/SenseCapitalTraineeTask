using IdentityModel.Client;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.Exceptions;
using SenseCapitalTraineeTask.Features.Auth.VerifyUser;

namespace SenseCapitalTraineeTask.Features.Auth.GetToken;

/// <summary>
/// Логика получения JWT с identity server
/// </summary>
[UsedImplicitly]
public class GetTokenHandler : IRequestHandler<GetTokenQuery, string>
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;
    private readonly ILogger<GetTokenHandler> _logger;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="httpClientFactory">Клиент</param>
    /// <param name="mediator">Медиатор</param>
    /// <param name="configuration">Конфигурация</param>
    /// <param name="logger"></param>
    public GetTokenHandler(
        IHttpClientFactory httpClientFactory,
        IMediator mediator,
        IConfiguration configuration,
        ILogger<GetTokenHandler> logger)
    {
        _httpClientFactory = httpClientFactory;
        _mediator = mediator;
        _configuration = configuration;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<string> Handle(GetTokenQuery request, CancellationToken cancellationToken)
    {
        var isExist = await _mediator.Send(new VerifyUserQuery(request.UserRequestDto), cancellationToken);

        if (!isExist)
        {
            throw new ScException("Неправильный имя пользователя или пароль");
        }
        
        var authClient = _httpClientFactory.CreateClient();

        _logger.LogInformation("Обращение к документации identity server");
        
        var discovery = await authClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
        {
            Address = Environment.GetEnvironmentVariable("ASPNETCORE_IDENTITY_URL") ?? _configuration["Auth:Authority"],
            Policy =
            {
                RequireHttps = false
            }
        }, cancellationToken: cancellationToken);

        if (discovery.IsError)
        {
            throw new ScException($"Сервис авторизации по адресу {Environment.GetEnvironmentVariable("ASPNETCORE_IDENTITY_URL") ?? _configuration["Auth:Authority"]} временно недоступен");
        }
        
        _logger.LogInformation("Обращение к маршруту получения JWT");
        
        var response = await authClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = discovery.TokenEndpoint,
            ClientId = "client_id",
            ClientSecret = "client_secret",
            Scope = "MyApi"
        }, cancellationToken: cancellationToken);

        _logger.LogInformation("Ответ: {0}", response);
        
        return response.AccessToken;
    }
}