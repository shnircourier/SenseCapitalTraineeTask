using System.Text.Json;
using JetBrains.Annotations;
using MediatR;
using Polly;
using Polly.Retry;
using SC.Internship.Common.ScResult;
using SenseCapitalTraineeTask.Identity;

namespace SenseCapitalTraineeTask.Features.Images.ImageById;

/// <summary>
/// Получение картинки по Id у стороннего сервиса
/// </summary>
[UsedImplicitly]
public class ImageByIdHandler : IRequestHandler<ImageByIdRequest, ScResult<string>>
{
    private const int MaxRetries = 3;
    private readonly IdentityService _identityService;
    private readonly ILogger<ImageByIdHandler> _logger;
    private readonly AsyncRetryPolicy<ScResult<string>> _retryPolicy;

    /// <summary>
    /// Получение картинки по Id у стороннего сервиса
    /// </summary>
    /// <param name="identityService"></param>
    /// <param name="logger"></param>
    public ImageByIdHandler(IdentityService identityService, ILogger<ImageByIdHandler> logger)
    {
        _identityService = identityService;
        _logger = logger;
        _retryPolicy = Policy<ScResult<string>>.Handle<HttpRequestException>().RetryAsync(MaxRetries);
    }

    /// <inheritdoc />
    public async Task<ScResult<string>> Handle(ImageByIdRequest request, CancellationToken cancellationToken)
    {
        var client = await _identityService.GetAuthorizedClient();

        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var imageUrl = Environment.GetEnvironmentVariable("ASPNETCORE_IMAGES_URL");
            
            _logger.LogInformation("Запрос к сервису картинок");
            
            var response = await client.GetAsync(imageUrl + $"/images/{request.Id}", cancellationToken);
            
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