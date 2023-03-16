using MediatR;

namespace SenseCapitalTraineeTask.Features.Meetings.MeetingById;

public record GetMeetingByIdQuery(Guid Id) : IRequest<MeetingResponse>;