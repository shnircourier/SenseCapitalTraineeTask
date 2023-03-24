namespace SenseCapitalTraineeTask.Features.Meetings;

public class MeetingEventBody
{
    public string QueueName { get; set; }

    public EventType EventType { get; set; }

    public string DeletedId { get; set; }
}