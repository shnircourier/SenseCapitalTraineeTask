using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using SenseCapitalTraineeTask.Features.Meetings.Data;
using SenseCapitalTraineeTask.Features.Meetings.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings.MeetingById;

/// <summary>
/// Логика получения мероприятия по Id
/// </summary>
[UsedImplicitly]
public class GetMeetingByIdHandler : IRequestHandler<GetMeetingByIdRequest, MeetingResponseDto>
{
    private readonly IRepository<Meeting> _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repository">БД</param>
    /// <param name="mapper">Mapper</param>
    public GetMeetingByIdHandler(
        IRepository<Meeting> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<MeetingResponseDto> Handle(GetMeetingByIdRequest request, CancellationToken cancellationToken)
    {
        var meeting = await _repository.Get(request.Id);

        var response = _mapper.Map<MeetingResponseDto>(meeting);
        
        return response;
    }
}