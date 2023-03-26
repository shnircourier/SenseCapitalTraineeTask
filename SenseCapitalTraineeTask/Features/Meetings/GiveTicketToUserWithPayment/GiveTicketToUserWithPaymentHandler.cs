using AutoMapper;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.Exceptions;
using SenseCapitalTraineeTask.Features.Meetings.Data;
using SenseCapitalTraineeTask.Features.Meetings.Data.Entities;
using SenseCapitalTraineeTask.Features.Payments;
using SenseCapitalTraineeTask.Features.Payments.CreatePayment;
using SenseCapitalTraineeTask.Features.Payments.UpdatePayment;

namespace SenseCapitalTraineeTask.Features.Meetings.GiveTicketToUserWithPayment;

/// <summary>
/// Логика передачи билета пользователю за деньги
/// </summary>
[UsedImplicitly]
public class GiveTicketToUserWithPaymentHandler : IRequestHandler<GiveTicketToUserWithPaymentCommand, MeetingResponseDto>
{
    private readonly IRepository<Meeting> _repository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ILogger<GiveTicketToUserWithPaymentHandler> _logger;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repository"></param>
    /// <param name="mapper"></param>
    /// <param name="mediator"></param>
    /// <param name="logger"></param>
    public GiveTicketToUserWithPaymentHandler(
        IRepository<Meeting> repository,
        IMapper mapper, 
        IMediator mediator,
        ILogger<GiveTicketToUserWithPaymentHandler> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _mediator = mediator;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<MeetingResponseDto> Handle(GiveTicketToUserWithPaymentCommand request, CancellationToken cancellationToken)
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

        var description = $"Создание оплаты для пользователя {request.RequestDto.UserId}";
            
        _logger.LogInformation("Создание запроса на оплату");
            
        var newPayment = await _mediator.Send(new CreatePaymentCommand(description), cancellationToken);
            
        _logger.LogInformation("Оплата на рассмотрении: {0}", newPayment);
        
        var payment = newPayment.Result!;
        
        try
        {
            var ticket = meeting.Tickets.First(t => t.OwnerId is null);
        
            ticket.OwnerId = request.RequestDto.UserId;
        
            var index = meeting.Tickets.IndexOf(ticket);
        
            meeting.Tickets[index] = ticket;
        
            meeting.IsFull = !meeting.Tickets.Any(t => t.OwnerId is null);
        
            var response = _mapper.Map<MeetingResponseDto>(await _repository.Update(meeting));

            var paymentRequest = new UpdatePaymentRequest
            {
                Description = $"Оплата для пользователя {request.RequestDto.UserId} подтверждена",
                Id = payment.Id,
                State = PaymentState.Confirmed
            };

            var confirmedPayment = await _mediator.Send(new UpdatePaymentCommand(paymentRequest), cancellationToken);

            _logger.LogInformation("Оплата подтверждена: {0}", confirmedPayment);

            return response;
        }
        catch (Exception)
        {
            var paymentRequest = new UpdatePaymentRequest
            {
                Description = $"Оплата для пользователя {request.RequestDto.UserId} отклонена",
                Id = payment.Id,
                State = PaymentState.Canceled
            };

            // ReSharper disable once UnusedVariable
            var canceledPayment = await _mediator.Send(new UpdatePaymentCommand(paymentRequest), cancellationToken);
            throw new ScException("Ошибка при проведении транзакции оплаты");
        }
        
        
    }
}