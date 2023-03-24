using JetBrains.Annotations;
using MediatR;
using SenseCapitalTraineeTask.Data;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings.DeleteManyMeetingsByRoomId;

/// <summary>
/// Удаления мероприятий проходящих в определенных помещениях
/// </summary>
[UsedImplicitly]
public class DeleteManyMeetingsByRoomIdHandler : IRequestHandler<DeleteManyMeetingsByRoomIdCommand>
{
    private readonly IRepository<Meeting> _repository;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repository"></param>
    public DeleteManyMeetingsByRoomIdHandler(IRepository<Meeting> repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task Handle(DeleteManyMeetingsByRoomIdCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteManyMeetingByRoomId(request.RoomId);
    }
}