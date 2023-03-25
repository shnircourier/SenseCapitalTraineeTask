using JetBrains.Annotations;
using MediatR;

namespace SenseCapitalTraineeTask.Features.Meetings.GiveTicketToUserWithPayment;

/// <summary>
/// Команда выдачи билета пользователю за деньги
/// </summary>
/// <param name="RequestDto"></param>
[UsedImplicitly]
public record GiveTicketToUserWithPaymentCommand(TicketRequestDto RequestDto, string MeetingId) : IRequest<MeetingResponseDto>;