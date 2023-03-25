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
    private readonly RabbitMqSenderService _senderService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="senderService"></param>
    public DeleteManyMeetingsByRoomIdHandler(IRepository<Meeting> repository, RabbitMqSenderService senderService)
    {
        _repository = repository;
        _senderService = senderService;
    }

    /// <inheritdoc />
    public async Task Handle(DeleteManyMeetingsByRoomIdCommand request, CancellationToken cancellationToken)
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