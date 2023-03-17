using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.Exceptions;
using SenseCapitalTraineeTask.Data;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings.GiveTicketToUser;

[UsedImplicitly]
public class GiveTicketToUserHandler : IRequestHandler<GiveTicketToUserCommand, MeetingResponseDto>
{
    private readonly IRepository<Meeting> _repository;
    private readonly IMapper _mapper;

    public GiveTicketToUserHandler(IRepository<Meeting> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public Task<MeetingResponseDto> Handle(GiveTicketToUserCommand request, CancellationToken cancellationToken)
    {
        var meeting = _repository.Get(request.RequestDto.MeetingId);

        if (meeting is null)
        {
            throw new ScException("Мероприятие не найдено");
        }

        if (meeting.IsFull)
        {
            throw new ScException("Билеты закончились");
        }

        var ticket = meeting.Tickets.First(t => t.OwnerId is null);

        ticket.OwnerId = request.RequestDto.UserId;
        
        var index = meeting.Tickets.IndexOf(ticket);

        meeting.Tickets[index] = ticket;

        meeting.IsFull = !meeting.Tickets.Any(t => t.OwnerId is null);

        var response = _mapper.Map<MeetingResponseDto>(_repository.Update(meeting));
        
        return Task.FromResult(response);
    }
}