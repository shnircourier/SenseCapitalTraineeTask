using System.Text.Json;
using MediatR;
using SC.Internship.Common.Exceptions;
using SenseCapitalTraineeTask.Identity;

namespace SenseCapitalTraineeTask.Features.Images.ImageById;

public class ImageByIdHandler : IRequestHandler<ImageByIdQuery, string>
{
    private readonly IdentityService _identityService;

    public ImageByIdHandler(IdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<string> Handle(ImageByIdQuery request, CancellationToken cancellationToken)
    {
        var client = await _identityService.GetAuthorizedClient();

        var response = await client.GetAsync($"http://localhost:5152/images/{request.Id}", cancellationToken);

        var data = await response.Content.ReadAsStringAsync(cancellationToken);

        return data;
    }
}