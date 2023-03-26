using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.Exceptions;
using SenseCapitalTraineeTask.Features.Meetings.Data;
using SenseCapitalTraineeTask.Features.Meetings.Data.Entities;
using SenseCapitalTraineeTask.Features.Meetings.GiveTicketToUserWithPayment;

namespace SenseCapitalTraineeTask.Features.Meetings.GiveTicketToUser;

/// <summary>
/// Логика выдачи билетов пользователю
/// </summary>
[UsedImplicitly]
public class GiveTicketToUserHandler : IRequestHandler<GiveTicketToUserCommand, MeetingResponseDto>
{
    private readonly IRepository<Meeting> _repository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repository">Бд</param>
    /// <param name="mapper">Mapper</param>
    /// <param name="mediator"></param>
    public GiveTicketToUserHandler(IRepository<Meeting> repository, IMapper mapper, IMediator mediator)
    {
        _repository = repository;
        _mapper = mapper;
        _mediator = mediator;
    }

    /// <inheritdoc />
    public async Task<MeetingResponseDto> Handle(GiveTicketToUserCommand request, CancellationToken cancellationToken)
    {
        var meeting = await _repository.Get(request.MeetingId);
        
        if (meeting is null)
        {
            throw new ScException("Мероприятие не найдено");
        }
        
        if (meeting.IsFull)
        {
            throw new ScException("Билеты закончились");
        }

        if (meeting.TicketPrice > 0)
        {
            return await _mediator.Send(new GiveTicketToUserWithPaymentCommand(request.RequestDto, request.MeetingId), cancellationToken);
        }
        
        var ticket = meeting.Tickets.First(t => t.OwnerId is null);
        
        ticket.OwnerId = request.RequestDto.UserId;
        
        var index = meeting.Tickets.IndexOf(ticket);
        
        meeting.Tickets[index] = ticket;
        
        meeting.IsFull = !meeting.Tickets.Any(t => t.OwnerId is null);
        
        var response = _mapper.Map<MeetingResponseDto>(await _repository.Update(meeting));
        
        return response;
    }
}