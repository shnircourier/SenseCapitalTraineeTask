namespace SenseCapitalTraineeTask.Features.Meetings;

/// <summary>
/// Модель тела запроса биела
/// </summary>
/// <param name="MeetingId">Идентификатор мероприятия</param>
/// <param name="UserId">Идентификатор пользователя</param>
public record TicketRequestDto(string MeetingId, string UserId);