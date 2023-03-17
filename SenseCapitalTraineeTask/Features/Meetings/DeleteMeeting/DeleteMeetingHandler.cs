using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.Exceptions;
using SenseCapitalTraineeTask.Data;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings.DeleteMeeting;

[UsedImplicitly]
public class DeleteMeetingHandler : IRequestHandler<DeleteMeetingCommand, MeetingResponseDto>
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