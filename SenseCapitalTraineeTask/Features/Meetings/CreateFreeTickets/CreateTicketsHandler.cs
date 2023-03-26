using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using SenseCapitalTraineeTask.Features.Meetings.Data;
using SenseCapitalTraineeTask.Features.Meetings.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings.CreateFreeTickets;

/// <summary>
/// Логика создания билетов
/// </summary>
[UsedImplicitly]
public class CreateTicketsHandler : IRequestHandler<CreateTicketsCommand, MeetingResponseDto>
{
    private readonly IRepository<Meeting> _meetingRepository;
    private readonly IRepository<Ticket> _ticketRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="meetingRepository">Коллекция мероприятий</param>
    /// <param name="ticketRepository">Коллекция билетов</param>
    /// <param name="mapper">Mapper</param>
    public CreateTicketsHandler(
        IRepository<Meeting> meetingRepository,
        IRepository<Ticket> ticketRepository,
        IMapper mapper)
    {
        _meetingRepository = meetingRepository;
        _ticketRepository = ticketRepository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<MeetingResponseDto> Handle(CreateTicketsCommand request, CancellationToken cancellationToken)
    {
        var meeting = await _meetingRepository.Get(request.MeetingId);

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

        meeting.IsFull = false;
        
        var newMeetingWithTickets = _mapper.Map<MeetingResponseDto>(await _meetingRepository.Update(meeting));
        
        return newMeetingWithTickets;
    }
}