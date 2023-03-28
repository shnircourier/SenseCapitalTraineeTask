using JetBrains.Annotations;
using MediatR;
using SenseCapitalTraineeTask.Rooms.Features.Data;
using SenseCapitalTraineeTask.Rooms.Features.Data.Entities;

namespace SenseCapitalTraineeTask.Rooms.Features.RoomList;

[UsedImplicitly]
public class RoomListHandler : IRequestHandler<RoomListRequest, List<string>>
{
    private readonly IRepository<Room> _repository;

    public RoomListHandler(IRepository<Room> repository)
    {
        _repository = repository;
    }
    
    public async Task<List<string>> Handle(RoomListRequest request, CancellationToken cancellationToken)
    {
        var result = await _repository.Get();

        return result.Select(r => r.Id).ToList()!;
    }
}