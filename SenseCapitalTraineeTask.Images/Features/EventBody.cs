using JetBrains.Annotations;

namespace SenseCapitalTraineeTask.Images.Features;

public class EventBody
{
#pragma warning disable CS8618
    public string QueueName { [UsedImplicitly] get; set; }
#pragma warning restore CS8618

    public EventType EventType { [UsedImplicitly] get; set; }

#pragma warning disable CS8618
    public string DeletedId { [UsedImplicitly] get; set; }
#pragma warning restore CS8618
}