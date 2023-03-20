namespace SenseCapitalTraineeTask.Features.Meetings;

/// <summary>
/// Модель билета
/// </summary>
public class TicketDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    // Id инициализируется неявным образом
    public string Id { get; set; }

    /// <summary>
    /// Идентификатор владельца
    /// </summary>
    public string? OwnerId { get; set; }

    /// <summary>
    /// Место
    /// </summary>
    public int Seat { get; set; }
}