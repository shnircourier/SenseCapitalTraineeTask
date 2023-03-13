namespace BusinessLogic.Models;

public record MeetingResponse(
    Guid Id,
    DateTime BeginAt,
    DateTime EndAt,
    string Title,
    string Description,
    Guid ImgId,
    Guid RoomId
);