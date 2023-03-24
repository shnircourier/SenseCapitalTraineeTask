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
    private readonly AsyncRetryPolicy<ScResult<string>> _retryPolicy;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="identityService"></param>
    public RoomByIdHandler(IdentityService identityService)
    {
        _identityService = identityService;
        _retryPolicy = Policy<ScResult<string>>.Handle<HttpRequestException>().RetryAsync(MaxRetries);
    }

    /// <inheritdoc />
    public async Task<ScResult<string>> Handle(RoomByIdQuery request, CancellationToken cancellationToken)
    {
        var client = await _identityService.GetAuthorizedClient();

        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var roomUrl = Environment.GetEnvironmentVariable("ASPNETCORE_ROOMS_URL");

            var response = await client.GetAsync(roomUrl + $"/rooms/{request.Id}", cancellationToken);

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