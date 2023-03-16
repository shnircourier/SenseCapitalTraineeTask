using MediatR;

namespace SenseCapitalTraineeTask.Features.Meetings.UpdateMeeting;

public record UpdateMeetingCommand(MeetingRequest Meeting, Guid Id) : IRequest<MeetingResponse>;