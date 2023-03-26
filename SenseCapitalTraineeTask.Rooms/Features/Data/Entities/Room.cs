using JetBrains.Annotations;

namespace SenseCapitalTraineeTask.Rooms.Features.Data.Entities;

/// <summary>
/// Модель заглушки помещения
/// </summary>
public class Room
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public string? Id { get; [UsedImplicitly] set; }
}