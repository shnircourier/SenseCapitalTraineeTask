namespace SenseCapitalTraineeTask.Features.Meetings;

/// <summary>
/// Модель билета
/// </summary>
public class TicketDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
#pragma warning disable CS8618
    public string Id { get; set; }
#pragma warning restore CS8618

    /// <summary>
    /// Идентификатор владельца
    /// </summary>
    public string? OwnerId { get; set; }

    /// <summary>
    /// Место
    /// </summary>
    public int Seat { get; set; }
}