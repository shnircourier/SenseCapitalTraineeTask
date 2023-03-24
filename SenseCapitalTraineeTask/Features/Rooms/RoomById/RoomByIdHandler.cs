using System.Text.Json;
using MediatR;
using SC.Internship.Common.ScResult;
using SenseCapitalTraineeTask.Identity;

namespace SenseCapitalTraineeTask.Features.Rooms.RoomById;

public class RoomByIdHandler : IRequestHandler<RoomByIdQuery, ScResult<string>>
{
    private readonly IdentityService _identityService;

    public RoomByIdHandler(IdentityService identityService)
    {
        _identityService = identityService;
    }
    
    public async Task<ScResult<string>> Handle(RoomByIdQuery request, CancellationToken cancellationToken)
    {
        var client = await _identityService.GetAuthorizedClient();

        var response = await client.GetAsync($"http://localhost:5290/rooms/{request.Id}", cancellationToken);

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var data = JsonSerializer.Deserialize<ScResult<string>>(content, options);

        return data!;
    }
}