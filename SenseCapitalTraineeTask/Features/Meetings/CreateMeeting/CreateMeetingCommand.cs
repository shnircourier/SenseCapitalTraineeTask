using MediatR;

namespace SenseCapitalTraineeTask.Features.Meetings.CreateMeeting;

public record CreateMeetingCommand(MeetingRequest Meeting) : IRequest<MeetingResponse>;