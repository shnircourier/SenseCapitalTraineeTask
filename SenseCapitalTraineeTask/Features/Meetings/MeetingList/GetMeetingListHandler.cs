using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using SenseCapitalTraineeTask.Features.Meetings.Data;
using SenseCapitalTraineeTask.Features.Meetings.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings.MeetingList;

/// <summary>
/// Логика получения списка мероприятий
/// </summary>
[UsedImplicitly]
public class GetMeetingListHandler : IRequestHandler<GetMeetingListRequest, List<MeetingResponseDto>>
{
    private readonly IRepository<Meeting> _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repository">БД</param>
    /// <param name="mapper">Mapper</param>
    public GetMeetingListHandler(
        IRepository<Meeting> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<List<MeetingResponseDto>> Handle(GetMeetingListRequest request, CancellationToken cancellationToken)
    {
        var response = _mapper.Map<List<MeetingResponseDto>>(await _repository.Get());
        
        return response;
    }
}