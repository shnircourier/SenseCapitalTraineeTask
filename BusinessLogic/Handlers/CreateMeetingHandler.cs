using AutoMapper;
using BusinessLogic.Commands;
using BusinessLogic.Models;
using Data;
using Data.Entities;
using MediatR;

namespace BusinessLogic.Handlers;

public class CreateMeetingHandler : IRequestHandler<CreateMeetingCommand, MeetingResponse>
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
    
    public Task<MeetingResponse> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = _mapper.Map<Meeting>(request.Meeting);
        
        var response = _mapper.Map<MeetingResponse>(_repository.Create(meeting));
        
        return Task.FromResult(response);
    }
}