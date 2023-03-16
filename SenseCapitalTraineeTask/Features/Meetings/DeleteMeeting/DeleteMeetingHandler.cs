using AutoMapper;
using MediatR;
using SenseCapitalTraineeTask.Data;
using SenseCapitalTraineeTask.Data.Entities;
using SenseCapitalTraineeTask.Infrastructure.Exceptions;

namespace SenseCapitalTraineeTask.Features.Meetings.DeleteMeeting;

public class DeleteMeetingHandler : IRequestHandler<DeleteMeetingCommand, MeetingResponse>
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
    
    public Task<MeetingResponse> Handle(DeleteMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = _repository.Delete(request.Id);
        
        if (meeting is null)
        {
            throw new NotFoundException("Мероприятие не найдено");
        }

        var response = _mapper.Map<MeetingResponse>(meeting);
        
        return Task.FromResult(response);
    }
}