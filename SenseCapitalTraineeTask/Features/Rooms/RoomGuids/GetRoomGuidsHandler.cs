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
    private readonly IRepository<Meeting> _repository;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repository">БД</param>
    public GetRoomGuidsHandler(IRepository<Meeting> repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public Task<RoomGuidsResponseDto> Handle(GetRoomGuidsQuery request, CancellationToken cancellationToken)
    {
        var response = _repository.GetAvailableRoomGuids();
        
        return Task.FromResult(new RoomGuidsResponseDto(response));
    }
}