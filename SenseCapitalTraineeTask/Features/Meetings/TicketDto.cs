using JetBrains.Annotations;

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
    [UsedImplicitly]
    public string? OwnerId { get; set; }

    /// <summary>
    /// Место
    /// </summary>
    [UsedImplicitly]
    public int Seat { get; set; }
}