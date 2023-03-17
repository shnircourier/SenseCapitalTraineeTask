using AutoMapper;
using MediatR;
using SenseCapitalTraineeTask.Data;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings.CreateMeeting;

public class CreateMeetingHandler : IRequestHandler<CreateMeetingCommand, MeetingResponseDto>
{
    private readonly IRepository<Meeting> _repository;
    private readonly IMapper _mapper;

    public CreateMeetingHandler(
        IRepository<Meeting> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public Task<MeetingResponseDto> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = _mapper.Map<Meeting>(request.Meeting);
        
        var response = _mapper.Map<MeetingResponseDto>(_repository.Create(meeting));
        
        return Task.FromResult(response);
    }
}