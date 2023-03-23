using MediatR;
using SenseCapitalTraineeTask.Rooms.Data;
using SenseCapitalTraineeTask.Rooms.Data.Entities;

namespace SenseCapitalTraineeTask.Rooms.Features.RoomList;

public class RoomListHandler : IRequestHandler<RoomListQuery, List<string>>
{
    private readonly IRepository<Room> _repository;

    public RoomListHandler(IRepository<Room> repository)
    {
        _repository = repository;
    }
    
    public async Task<List<string>> Handle(RoomListQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.Get();

        return result.Select(r => r.Id).ToList();
    }
}