namespace SenseCapitalTraineeTask.Features.Meetings;

/// <summary>
/// Модель создания билетов
/// </summary>
/// <param name="Amount">Кол-во билетов (мест)</param>
/// <param name="IsSeatRequired">Нужен ли номер места</param>
public record CreateTicketsRequestDto(int Amount, bool IsSeatRequired);