using MediatR;

namespace SenseCapitalTraineeTask.Features.Meetings.CheckUserTicket;

public record CheckUserTicketQuery(string MeetingId, CheckTicketRequestDto RequestDto) : IRequest;