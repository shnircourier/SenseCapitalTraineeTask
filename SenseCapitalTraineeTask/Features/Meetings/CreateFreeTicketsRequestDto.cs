namespace SenseCapitalTraineeTask.Features.Meetings;

/// <summary>
/// Модель создания билетов
/// </summary>
/// <param name="Id">Id мероприятия</param>
/// <param name="Amount">Кол-во билетов(мест)</param>
public record CreateFreeTicketsRequestDto(Guid Id, int Amount);