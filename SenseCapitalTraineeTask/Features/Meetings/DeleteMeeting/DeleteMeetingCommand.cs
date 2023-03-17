using MediatR;

namespace SenseCapitalTraineeTask.Features.Meetings.DeleteMeeting;

public record DeleteMeetingCommand(Guid Id) : IRequest<MeetingResponseDto>;