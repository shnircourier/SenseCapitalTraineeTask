namespace SenseCapitalTraineeTask.Features.Meetings;

/// <summary>
/// Модель билета
/// </summary>
public class TicketDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор владельца
    /// </summary>
    public Guid OwnerId { get; set; }

    /// <summary>
    /// Место
    /// </summary>
    public int Seat { get; set; }
}