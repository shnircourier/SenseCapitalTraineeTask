using JetBrains.Annotations;

namespace SenseCapitalTraineeTask.Rooms.Features;

public enum EventType
{
    SpaceDeleteEvent = 1,
    [UsedImplicitly] ImageDeleteEvent = 2,
    [UsedImplicitly] MeetingDeleteEvent = 3
}