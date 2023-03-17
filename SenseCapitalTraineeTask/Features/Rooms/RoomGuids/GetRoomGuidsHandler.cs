using MediatR;
using SenseCapitalTraineeTask.Data;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Rooms.RoomGuids;

public class GetRoomGuidsHandler : IRequestHandler<GetRoomGuidsQuery, RoomGuidsResponseDto>
{
    private readonly IRepository<Meeting> _repository;

    public GetRoomGuidsHandler(IRepository<Meeting> repository)
    {
        _repository = repository;
    }
    
    public Task<RoomGuidsResponseDto> Handle(GetRoomGuidsQuery request, CancellationToken cancellationToken)
    {
        var response = _repository.GetAvailableRoomGuids();
        
        return Task.FromResult(new RoomGuidsResponseDto(response));
    }
}