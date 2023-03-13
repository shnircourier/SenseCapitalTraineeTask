using AutoMapper;
using BusinessLogic.Models;
using BusinessLogic.Queries;
using Data;
using Data.Entities;
using MediatR;

namespace BusinessLogic.Handlers;

public class GetMeetingByIdHandler : IRequestHandler<GetMeetingByIdQuery, MeetingResponse>
{
    private readonly IRepository<Meeting> _repository;
    private readonly IMapper _mapper;

    public Guid Id { get; set; }

    public GetMeetingByIdHandler(
        IRepository<Meeting> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public Task<MeetingResponse> Handle(GetMeetingByIdQuery request, CancellationToken cancellationToken)
    {
        var response = _mapper.Map<MeetingResponse>(_repository.Get(Id));
        
        return Task.FromResult(response);
    }
}