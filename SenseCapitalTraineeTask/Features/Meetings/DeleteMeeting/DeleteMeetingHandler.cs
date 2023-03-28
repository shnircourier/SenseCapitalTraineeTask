using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using SenseCapitalTraineeTask.Features.Meetings.Data;
using SenseCapitalTraineeTask.Features.Meetings.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings.DeleteMeeting;

/// <summary>
/// Логика удаления мероприятия
/// </summary>
[UsedImplicitly]
public class DeleteMeetingHandler : IRequestHandler<DeleteMeetingRequest, MeetingResponseDto>
{
    private readonly IRepository<Meeting> _repository;
    private readonly IMapper _mapper;
    private readonly MeetingsSenderService _senderService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repository">БД</param>
    /// <param name="mapper">Mapper</param>
    /// <param name="senderService"></param>
    public DeleteMeetingHandler(
        IRepository<Meeting> repository,
        IMapper mapper,
        MeetingsSenderService senderService)
    {
        _repository = repository;
        _mapper = mapper;
        _senderService = senderService;
    }

    /// <inheritdoc />
    public async Task<MeetingResponseDto> Handle(DeleteMeetingRequest request, CancellationToken cancellationToken)
    {
        var meeting = await _repository.Get(request.Id);

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