using MediatR;
using SenseCapitalTraineeTask.Identity;

namespace SenseCapitalTraineeTask.Features.Rooms.RoomById;

public class RoomByIdHandler : IRequestHandler<RoomByIdQuery, string>
{
    private readonly IdentityService _identityService;

    public RoomByIdHandler(IdentityService identityService)
    {
        _identityService = identityService;
    }
    
    public async Task<string> Handle(RoomByIdQuery request, CancellationToken cancellationToken)
    {
        var client = await _identityService.GetAuthorizedClient();

        var response = await client.GetAsync($"http://localhost:5152/rooms/{request.Id}", cancellationToken);

        var data = await response.Content.ReadAsStringAsync(cancellationToken);

        return data;
    }
}