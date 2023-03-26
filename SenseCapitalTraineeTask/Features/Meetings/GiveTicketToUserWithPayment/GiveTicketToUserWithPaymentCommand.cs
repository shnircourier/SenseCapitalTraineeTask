using JetBrains.Annotations;
using MediatR;

namespace SenseCapitalTraineeTask.Features.Meetings.GiveTicketToUserWithPayment;

/// <summary>
/// Команда выдачи билета пользователю за деньги
/// </summary>
/// <param name="RequestDto"></param>
/// <param name="MeetingId"></param>
[UsedImplicitly]
public record GiveTicketToUserWithPaymentCommand(TicketRequestDto RequestDto, string MeetingId) : IRequest<MeetingResponseDto>;