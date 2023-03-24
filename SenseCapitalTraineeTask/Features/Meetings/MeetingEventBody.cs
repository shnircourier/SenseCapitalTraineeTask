namespace SenseCapitalTraineeTask.Features.Meetings;

/// <summary>
/// Тело сообщения события
/// </summary>
public class MeetingEventBody
{
#pragma warning disable CS8618
    /// <summary>
    /// Имя очереди
    /// </summary>
    public string QueueName { get; set; }

    /// <summary>
    /// Тип события
    /// </summary>
    public EventType EventType { get; set; }

    /// <summary>
    /// Id события
    /// </summary>
    public string DeletedId { get; set; }
    
#pragma warning restore CS8618
}