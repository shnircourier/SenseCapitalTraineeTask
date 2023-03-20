namespace SenseCapitalTraineeTask.Features.Meetings;

public record CheckTicketRequestDto(string UserId, string TicketId, int? Seat);