using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings;

public record MeetingResponseDto(
    Guid Id,
    DateTime BeginAt,
    DateTime EndAt,
    string Title,
    string Description,
    Guid ImgId,
    Guid RoomId,
    List<Ticket> Tickets,
    bool IsFull);