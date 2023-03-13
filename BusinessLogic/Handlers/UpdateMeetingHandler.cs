using AutoMapper;
using BusinessLogic.Commands;
using BusinessLogic.Models;
using Data;
using Data.Entities;
using MediatR;

namespace BusinessLogic.Handlers;

public class UpdateMeetingHandler : IRequestHandler<UpdateMeetingCommand, MeetingResponse>
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
    
    public Task<MeetingResponse> Handle(UpdateMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = _repository.Get(request.Id);

        meeting.Description = request.Meeting.Description;

        meeting.Title = request.Meeting.Title;

        meeting.BeginAt = request.Meeting.BeginAt;

        meeting.BeginAt = request.Meeting.EndAt;

        meeting.ImgId = request.Meeting.ImgId;

        meeting.RoomId = request.Meeting.RoomId;

        var response = _mapper.Map<MeetingResponse>(_repository.Update(meeting));

        return Task.FromResult(response);
    }
}