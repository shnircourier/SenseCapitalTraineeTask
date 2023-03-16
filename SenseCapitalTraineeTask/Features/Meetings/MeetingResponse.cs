namespace SenseCapitalTraineeTask.Features.Meetings;

public record MeetingResponse(
    Guid Id,
    DateTime BeginAt,
    DateTime EndAt,
    string Title,
    string Description,
    Guid ImgId,
    Guid RoomId
);