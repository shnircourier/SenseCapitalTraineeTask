using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.Exceptions;
using SenseCapitalTraineeTask.Data;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings.MeetingById;

/// <summary>
/// Логика получения мероприятия по Id
/// </summary>
[UsedImplicitly]
public class GetMeetingByIdHandler : IRequestHandler<GetMeetingByIdQuery, MeetingResponseDto>
{
    private readonly IRepository<Meeting> _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repository">БД</param>
    /// <param name="mapper">маппер</param>
    public GetMeetingByIdHandler(
        IRepository<Meeting> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public Task<MeetingResponseDto> Handle(GetMeetingByIdQuery request, CancellationToken cancellationToken)
    {
        var response = _mapper.Map<MeetingResponseDto>(_repository.Get(request.Id));

        if (response is null)
        {
            throw new ScException("Мероприятие не найдено");
        }
        
        return Task.FromResult(response);
    }
}