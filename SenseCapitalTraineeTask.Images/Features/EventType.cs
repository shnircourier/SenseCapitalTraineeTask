using JetBrains.Annotations;

namespace SenseCapitalTraineeTask.Images.Features;

public enum EventType
{
    [UsedImplicitly] SpaceDeleteEvent = 1,
    ImageDeleteEvent = 2,
    [UsedImplicitly] MeetingDeleteEvent = 3
}