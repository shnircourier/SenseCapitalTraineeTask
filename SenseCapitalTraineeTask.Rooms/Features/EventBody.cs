namespace SenseCapitalTraineeTask.Rooms.Features;

public class EventBody
{
    public string QueueName { get; set; }

    public EventType EventType { get; set; }

    public string DeletedId { get; set; }
}