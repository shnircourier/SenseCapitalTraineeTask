using AutoMapper;
using MediatR;
using SenseCapitalTraineeTask.Data;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings.MeetingList;

public class GetMeetingListHandler : IRequestHandler<GetMeetingListQuery, List<MeetingResponseDto>>
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
    
    public Task<List<MeetingResponseDto>> Handle(GetMeetingListQuery request, CancellationToken cancellationToken)
    {
        var response = _mapper.Map<List<MeetingResponseDto>>(_repository.Get());
        
        return Task.FromResult(response);
    }
}