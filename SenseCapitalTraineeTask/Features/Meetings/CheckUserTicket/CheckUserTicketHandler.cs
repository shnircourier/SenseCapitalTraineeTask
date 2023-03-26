using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.Exceptions;
using SenseCapitalTraineeTask.Features.Meetings.Data;
using SenseCapitalTraineeTask.Features.Meetings.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings.CheckUserTicket;

/// <summary>
/// Логика проверки билета пользователя
/// </summary>
[UsedImplicitly]
public class CheckUserTicketHandler : IRequestHandler<CheckUserTicketRequest>
{
    private readonly IRepository<Meeting> _repository;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repository">бд</param>
    public CheckUserTicketHandler(IRepository<Meeting> repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task Handle(CheckUserTicketRequest request, CancellationToken cancellationToken)
    {
        var meeting = await _repository.Get(request.MeetingId);

        if (meeting is null)
        {
            throw new ScException("Мероприятие не найдено");
        }

        var ticket = meeting.Tickets.FirstOrDefault(t => t.Id == request.RequestDto.TicketId);
        
        if (ticket is null)
        {
            throw new ScException($"Билет с идентификатором {request.RequestDto.TicketId} не найден");
        }
        
        if (ticket.OwnerId is not null && ticket.OwnerId != request.RequestDto.UserId)
        {
            throw new ScException("Данный билет принадлежит другому владельцу");
        }

        var isSeatRequired = meeting.Tickets.Any(t => t.Seat is not null);

        if (isSeatRequired && request.RequestDto.Seat != ticket.Seat)
        {
            throw new ScException("Места не совпадают");
        }
    }
}