using AutoMapper;
using MediatR;
using SC.Internship.Common.Exceptions;
using SenseCapitalTraineeTask.Data;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings.CreateFreeTickets;

public class CreateFreeTicketsHandler : IRequestHandler<CreateFreeTicketsCommand, MeetingResponseDto>
{
    private readonly IRepository<Meeting> _repository;
    private readonly IMapper _mapper;

    public CreateFreeTicketsHandler(
        IRepository<Meeting> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public Task<MeetingResponseDto> Handle(CreateFreeTicketsCommand request, CancellationToken cancellationToken)
    {
        var meeting = _repository.Get(request.RequestDto.Id);
        
        if (meeting is null)
        {
            throw new ScException("Мероприятие не найдено");
        }

        var newFreeTickets = new List<Ticket>();

        for (var i = 0; i < request.RequestDto.Amount; i++)
        {
            newFreeTickets.Add(new Ticket
            {
                Id = Guid.NewGuid(),
                OwnerId = null,
                Seat = i + 1
            });
        }

        meeting.Tickets = newFreeTickets;

        var newMeetingWithTickets = _mapper.Map<MeetingResponseDto>(_repository.Update(meeting));

        return Task.FromResult(newMeetingWithTickets);
    }
}