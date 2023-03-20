namespace SenseCapitalTraineeTask.Features.Meetings;

/// <summary>
/// Модель создания билетов
/// </summary>
/// <param name="Amount">Кол-во билетов (мест)</param>
public record CreateFreeTicketsRequestDto(int Amount, bool IsSeatRequired);