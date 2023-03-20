namespace SenseCapitalTraineeTask.Features.Meetings;

/// <summary>
/// Модель проверки билета пользователя
/// </summary>
/// <param name="UserId"></param>
/// <param name="TicketId"></param>
/// <param name="Seat"></param>
public record CheckTicketRequestDto(string UserId, string TicketId, int? Seat);