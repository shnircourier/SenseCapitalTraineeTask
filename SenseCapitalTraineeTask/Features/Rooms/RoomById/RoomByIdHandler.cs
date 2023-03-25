using System.Text.Json;
using JetBrains.Annotations;
using MediatR;
using Polly;
using Polly.Retry;
using SC.Internship.Common.ScResult;
using SenseCapitalTraineeTask.Identity;

namespace SenseCapitalTraineeTask.Features.Rooms.RoomById;

/// <summary>
/// Обращение к стороннему сервису помещений
/// </summary>
[UsedImplicitly]
public class RoomByIdHandler : IRequestHandler<RoomByIdQuery, ScResult<string>>
{
    private const int MaxRetries = 3;
    private readonly IdentityService _identityService;
    private readonly ILogger<RoomByIdHandler> _logger;
    private readonly AsyncRetryPolicy<ScResult<string>> _retryPolicy;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="identityService"></param>
    /// <param name="logger"></param>
    public RoomByIdHandler(IdentityService identityService, ILogger<RoomByIdHandler> logger)
    {
        _identityService = identityService;
        _logger = logger;
        _retryPolicy = Policy<ScResult<string>>.Handle<HttpRequestException>().RetryAsync(MaxRetries);
    }

    /// <inheritdoc />
    public async Task<ScResult<string>> Handle(RoomByIdQuery request, CancellationToken cancellationToken)
    {
        var client = await _identityService.GetAuthorizedClient();

        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var roomUrl = Environment.GetEnvironmentVariable("ASPNETCORE_ROOMS_URL");
            
            _logger.LogInformation("Обращение к сервису помещений");

            var response = await client.GetAsync(roomUrl + $"/rooms/{request.Id}", cancellationToken);
            
            _logger.LogInformation("Ответ: {0}", response);

            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var data = JsonSerializer.Deserialize<ScResult<string>>(content, options);

            return data!;
        });
    }
}