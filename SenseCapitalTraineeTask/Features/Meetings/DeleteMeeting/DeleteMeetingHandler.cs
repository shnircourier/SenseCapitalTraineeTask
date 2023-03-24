using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.Exceptions;
using SenseCapitalTraineeTask.Data;
using SenseCapitalTraineeTask.Data.Entities;
using SenseCapitalTraineeTask.Infrastructure;

namespace SenseCapitalTraineeTask.Features.Meetings.DeleteMeeting;

/// <summary>
/// Логика удаления мероприятия
/// </summary>
[UsedImplicitly]
public class DeleteMeetingHandler : IRequestHandler<DeleteMeetingCommand, MeetingResponseDto>
{
    private readonly IRepository<Meeting> _repository;
    private readonly IMapper _mapper;
    private readonly RabbitMqSenderService _senderService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repository">БД</param>
    /// <param name="mapper">Маппер</param>
    public DeleteMeetingHandler(
        IRepository<Meeting> repository,
        IMapper mapper,
        RabbitMqSenderService senderService)
    {
        _repository = repository;
        _mapper = mapper;
        _senderService = senderService;
    }

    /// <inheritdoc />
    public async Task<MeetingResponseDto> Handle(DeleteMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = await _repository.Get(request.Id);

        if (meeting is null)
        {
            throw new ScException("Мероприятие не найдено");
        }
        
        await _repository.Delete(meeting);
        
        _senderService.SendingMessage(new MeetingEventBody
        {
            DeletedId = meeting.Id,
            EventType = EventType.MeetingDeleteEvent,
            QueueName = "MeetingDeleteEvent"
        });

        var response = _mapper.Map<MeetingResponseDto>(meeting);
        
        return response;
    }
}