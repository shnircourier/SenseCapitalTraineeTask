using JetBrains.Annotations;

namespace SenseCapitalTraineeTask.Features.Meetings;

/// <summary>
/// Тип события
/// </summary>
public enum EventType
{
    /// <summary>
    /// Событие удаления пространства
    /// </summary>
    [UsedImplicitly] SpaceDeleteEvent = 1,
    /// <summary>
    /// Событие удаления картинки
    /// </summary>
    [UsedImplicitly] ImageDeleteEvent = 2,
    /// <summary>
    /// Событие удаления события
    /// </summary>
    MeetingDeleteEvent = 3
}