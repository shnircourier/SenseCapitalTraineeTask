using AutoMapper;
using BusinessLogic.Models;
using BusinessLogic.Queries;
using Data;
using Data.Entities;
using MediatR;

namespace BusinessLogic.Handlers;

public class GetMeetingListHandler : IRequestHandler<GetMeetingListQuery, List<MeetingResponse>>
{
    private readonly IRepository<Meeting> _repository;
    private readonly IMapper _mapper;

    public GetMeetingListHandler(
        IRepository<Meeting> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public Task<List<MeetingResponse>> Handle(GetMeetingListQuery request, CancellationToken cancellationToken)
    {
        var response = _mapper.Map<List<MeetingResponse>>(_repository.Get());
        
        return Task.FromResult(response);
    }
}