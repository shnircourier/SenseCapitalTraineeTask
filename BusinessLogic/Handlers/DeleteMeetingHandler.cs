using AutoMapper;
using BusinessLogic.Commands;
using BusinessLogic.Models;
using Data;
using Data.Entities;
using MediatR;

namespace BusinessLogic.Handlers;

public class DeleteMeetingHandler : IRequestHandler<DeleteMeetingCommand, MeetingResponse>
{
    private readonly IRepository<Meeting> _repository;
    private readonly IMapper _mapper;

    public DeleteMeetingHandler(
        IRepository<Meeting> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public Task<MeetingResponse> Handle(DeleteMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = _repository.Delete(request.Id);

        var response = _mapper.Map<MeetingResponse>(meeting);
        
        return Task.FromResult(response);
    }
}