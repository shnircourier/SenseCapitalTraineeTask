using MediatR;

namespace SenseCapitalTraineeTask.Features.Meetings.UpdateMeeting;

public record UpdateMeetingCommand(MeetingRequestDto Meeting, Guid Id) : IRequest<MeetingResponseDto>;