using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.Exceptions;
using SenseCapitalTraineeTask.Data;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings.UpdateMeeting;

[UsedImplicitly]
public class UpdateMeetingHandler : IRequestHandler<UpdateMeetingCommand, MeetingResponseDto>
{
    private readonly IRepository<Meeting> _repository;
    private readonly IMapper _mapper;

    public UpdateMeetingHandler(
        IRepository<Meeting> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public Task<MeetingResponseDto> Handle(UpdateMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = _repository.Get(request.Id);

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

        var response = _mapper.Map<MeetingResponseDto>(_repository.Update(meeting));

        return Task.FromResult(response);
    }
}