using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.Exceptions;
using SenseCapitalTraineeTask.Data;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings.CreateFreeTickets;

/// <summary>
/// Логика создания билетов
/// </summary>
[UsedImplicitly]
public class CreateFreeTicketsHandler : IRequestHandler<CreateFreeTicketsCommand, MeetingResponseDto>
{
    private readonly IRepository<Meeting> _meetingRepository;
    private readonly IRepository<Ticket> _ticketRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="meetingRepository">Коллекция мероприятий</param>
    /// <param name="ticketRepository">Коллекция билетов</param>
    /// <param name="mapper">Маппер</param>
    public CreateFreeTicketsHandler(
        IRepository<Meeting> meetingRepository,
        IRepository<Ticket> ticketRepository,
        IMapper mapper)
    {
        _meetingRepository = meetingRepository;
        _ticketRepository = ticketRepository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<MeetingResponseDto> Handle(CreateFreeTicketsCommand request, CancellationToken cancellationToken)
    {
        var meeting = await _meetingRepository.Get(request.MeetingId);
        
        if (meeting is null)
        {
            throw new ScException("Мероприятие не найдено");
        }
        
        var newFreeTickets = new List<Ticket>();
        
        for (var i = 0; i < request.RequestDto.Amount; i++)
        {
            newFreeTickets.Add(new Ticket
            {
                OwnerId = null,
                Seat = request.RequestDto.IsSeatRequired? i + 1 : null
            });
        }
        
        meeting.Tickets = await _ticketRepository.CreateMany(newFreeTickets);
        
        var newMeetingWithTickets = _mapper.Map<MeetingResponseDto>(await _meetingRepository.Update(meeting));
        
        return newMeetingWithTickets;
    }
}