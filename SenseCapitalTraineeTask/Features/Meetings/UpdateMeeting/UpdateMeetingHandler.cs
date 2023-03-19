using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.Exceptions;
using SenseCapitalTraineeTask.Data;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings.UpdateMeeting;

/// <summary>
/// Логика обновление данных мероприятия
/// </summary>
[UsedImplicitly]
public class UpdateMeetingHandler : IRequestHandler<UpdateMeetingCommand, MeetingResponseDto>
{
    private readonly IRepository<Meeting> _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repository">БД</param>
    /// <param name="mapper">Маппер</param>
    public UpdateMeetingHandler(
        IRepository<Meeting> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<MeetingResponseDto> Handle(UpdateMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = await _repository.Get(request.Id);

        if (meeting is null)
        {
            throw new ScException("Мероприятие не найдено");
        }

        meeting.Description = request.Meeting.Description;

        meeting.Title = request.Meeting.Title;

        meeting.BeginAt = request.Meeting.BeginAt;

        meeting.BeginAt = request.Meeting.EndAt;

        meeting.ImgId = request.Meeting.ImgId;

        meeting.RoomId = request.Meeting.RoomId;

        var response = _mapper.Map<MeetingResponseDto>(await _repository.Update(meeting));

        return response;
    }
}