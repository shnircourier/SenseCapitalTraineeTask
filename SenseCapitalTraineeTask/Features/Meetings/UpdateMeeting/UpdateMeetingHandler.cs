using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using SenseCapitalTraineeTask.Features.Meetings.Data;
using SenseCapitalTraineeTask.Features.Meetings.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings.UpdateMeeting;

/// <summary>
/// Логика обновление данных мероприятия
/// </summary>
[UsedImplicitly]
public class UpdateMeetingHandler : IRequestHandler<UpdateMeetingRequest, MeetingResponseDto>
{
    private readonly IRepository<Meeting> _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repository">БД</param>
    /// <param name="mapper">Mapper</param>
    public UpdateMeetingHandler(
        IRepository<Meeting> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<MeetingResponseDto> Handle(UpdateMeetingRequest request, CancellationToken cancellationToken)
    {
        var meeting = await _repository.Get(request.Id);

        meeting.Description = request.Meeting.Description;

        meeting.Title = request.Meeting.Title;

        meeting.BeginAt = request.Meeting.BeginAt;

        meeting.EndAt = request.Meeting.EndAt;

        meeting.ImgId = request.Meeting.ImgId;

        meeting.RoomId = request.Meeting.RoomId;

        meeting.TicketPrice = request.Meeting.TicketPrice;

        var response = _mapper.Map<MeetingResponseDto>(await _repository.Update(meeting));

        return response;
    }
}