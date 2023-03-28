using JetBrains.Annotations;
using MediatR;
using SenseCapitalTraineeTask.Features.Meetings.Data;
using SenseCapitalTraineeTask.Features.Meetings.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings.DeleteManyMeetingsByRoomId;

/// <summary>
/// Удаления мероприятий проходящих в определенных помещениях
/// </summary>
[UsedImplicitly]
public class DeleteManyMeetingsByRoomIdHandler : IRequestHandler<DeleteManyMeetingsByRoomIdRequest>
{
    private readonly IRepository<Meeting> _repository;
    private readonly MeetingsSenderService _senderService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="senderService"></param>
    public DeleteManyMeetingsByRoomIdHandler(IRepository<Meeting> repository, MeetingsSenderService senderService)
    {
        _repository = repository;
        _senderService = senderService;
    }

    /// <inheritdoc />
    public async Task Handle(DeleteManyMeetingsByRoomIdRequest request, CancellationToken cancellationToken)
    {
        await _repository.DeleteManyMeetingByRoomId(request.RoomId);

        var meetings = await _repository.GetMeetingsByRoomId(request.RoomId);
        
        meetings.ForEach(m =>
        {
            _senderService.SendingMessage(new MeetingEventBody
            {
                DeletedId = m.Id,
                EventType = EventType.MeetingDeleteEvent,
                QueueName = "MeetingDeleteEvent"
            });
        });
    }
}