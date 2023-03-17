namespace SenseCapitalTraineeTask.Features.Meetings;

public record MeetingRequestDto(
    DateTime BeginAt,
    DateTime EndAt,
    string Title,
    string Description,
    Guid ImgId,
    Guid RoomId
);