namespace SenseCapitalTraineeTask.Features.Meetings;

public record MeetingRequest(
    DateTime BeginAt,
    DateTime EndAt,
    string Title,
    string Description,
    Guid ImgId,
    Guid RoomId
);