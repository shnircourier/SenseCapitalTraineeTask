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

        if (meeting is null)
        {
            throw new ScException("Мероприятие не найдено");
        }
        
        var response = _mapper.Map<MeetingResponseDto>(meeting);
        
        return response;
    }
}