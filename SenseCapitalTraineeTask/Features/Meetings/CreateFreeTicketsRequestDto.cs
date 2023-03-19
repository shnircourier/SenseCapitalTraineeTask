namespace SenseCapitalTraineeTask.Features.Meetings;

/// <summary>
/// Модель создания билетов
/// </summary>
/// <param name="MeetingId">Id мероприятия</param>
/// <param name="Amount">Кол-во билетов(мест)</param>
public record CreateFreeTicketsRequestDto(string MeetingId, int Amount);