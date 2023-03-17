using MediatR;

namespace SenseCapitalTraineeTask.Features.Meetings.CreateMeeting;

public record CreateMeetingCommand(MeetingRequestDto Meeting) : IRequest<MeetingResponseDto>;