using MediatR;

namespace SenseCapitalTraineeTask.Features.Meetings.CheckUserTicket;

/// <summary>
/// Запрос на проверку подлинности билета пользователя
/// </summary>
/// <param name="MeetingId"></param>
/// <param name="RequestDto"></param>
public record CheckUserTicketRequest(string MeetingId, CheckTicketRequestDto RequestDto) : IRequest;