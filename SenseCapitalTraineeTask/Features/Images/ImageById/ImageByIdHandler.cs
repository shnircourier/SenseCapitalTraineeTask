using System.Text.Json;
using MediatR;
using SC.Internship.Common.ScResult;
using SenseCapitalTraineeTask.Identity;

namespace SenseCapitalTraineeTask.Features.Images.ImageById;

public class ImageByIdHandler : IRequestHandler<ImageByIdQuery, ScResult<string>>
{
    private readonly IdentityService _identityService;

    public ImageByIdHandler(IdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<ScResult<string>> Handle(ImageByIdQuery request, CancellationToken cancellationToken)
    {
        var client = await _identityService.GetAuthorizedClient();

        var response = await client.GetAsync($"http://localhost:5152/images/{request.Id}", cancellationToken);

        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var data = JsonSerializer.Deserialize<ScResult<string>>(content, options);

        return data!;
    }
}