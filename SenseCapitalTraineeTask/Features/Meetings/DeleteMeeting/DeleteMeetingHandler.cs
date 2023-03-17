using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.Exceptions;
using SenseCapitalTraineeTask.Data;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings.DeleteMeeting;

/// <summary>
/// Логика удаления мероприятия
/// </summary>
[UsedImplicitly]
public class DeleteMeetingHandler : IRequestHandler<DeleteMeetingCommand, MeetingResponseDto>
{
    private readonly IRepository<Meeting> _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repository">БД</param>
    /// <param name="mapper">Маппер</param>
    public DeleteMeetingHandler(
        IRepository<Meeting> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public Task<MeetingResponseDto> Handle(DeleteMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = _repository.Delete(request.Id);
        
        if (meeting is null)
        {
            throw new ScException("Мероприятие не найдено");
        }

        var response = _mapper.Map<MeetingResponseDto>(meeting);
        
        return Task.FromResult(response);
    }
}