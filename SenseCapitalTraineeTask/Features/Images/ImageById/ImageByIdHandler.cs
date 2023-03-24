using System.Text.Json;
using MediatR;
using Polly;
using Polly.Retry;
using SC.Internship.Common.ScResult;
using SenseCapitalTraineeTask.Identity;

namespace SenseCapitalTraineeTask.Features.Images.ImageById;

public class ImageByIdHandler : IRequestHandler<ImageByIdQuery, ScResult<string>>
{
    private const int MaxRetries = 3;
    private readonly IdentityService _identityService;
    private readonly AsyncRetryPolicy<ScResult<string>> _retryPolicy;

    public ImageByIdHandler(IdentityService identityService)
    {
        _identityService = identityService;
        _retryPolicy = Policy<ScResult<string>>.Handle<HttpRequestException>().RetryAsync(MaxRetries);
    }

    public async Task<ScResult<string>> Handle(ImageByIdQuery request, CancellationToken cancellationToken)
    {
        var client = await _identityService.GetAuthorizedClient();

        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var imageUrl = Environment.GetEnvironmentVariable("ASPNETCORE_IMAGES_URL");
            
            var response = await client.GetAsync(imageUrl + $"/images/{request.Id}", cancellationToken);

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