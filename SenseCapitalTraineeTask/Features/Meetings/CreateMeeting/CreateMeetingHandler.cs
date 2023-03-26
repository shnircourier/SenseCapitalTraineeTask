using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using SenseCapitalTraineeTask.Features.Meetings.Data;
using SenseCapitalTraineeTask.Features.Meetings.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings.CreateMeeting;

/// <summary>
/// Логика создания мероприятия
/// </summary>
[UsedImplicitly]
public class CreateMeetingHandler : IRequestHandler<CreateMeetingCommand, MeetingResponseDto>
{
    private readonly IRepository<Meeting> _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repository">БД</param>
    /// <param name="mapper">Mapper</param>
    public CreateMeetingHandler(
        IRepository<Meeting> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<MeetingResponseDto> Handle(CreateMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = _mapper.Map<Meeting>(request.Meeting);

        var response = _mapper.Map<MeetingResponseDto>(await _repository.Create(meeting));
        
        return response;
    }
}