using JetBrains.Annotations;
using MediatR;
using SenseCapitalTraineeTask.Data;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Rooms.RoomGuids;

/// <summary>
/// Логика получения множества id
/// </summary>
[UsedImplicitly]
public class GetRoomGuidsHandler : IRequestHandler<GetRoomGuidsQuery, RoomGuidsResponseDto>
{
    private readonly IRepository<Room> _repository;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repository">БД</param>
    public GetRoomGuidsHandler(IRepository<Room> repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<RoomGuidsResponseDto> Handle(GetRoomGuidsQuery request, CancellationToken cancellationToken)
    {
        var response = await _repository.Get();

        var rooms = response.Select(r => r.Id).ToHashSet();
        
        return new RoomGuidsResponseDto(rooms);
    }
}