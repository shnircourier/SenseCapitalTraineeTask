using MediatR;

namespace SenseCapitalTraineeTask.Features.Meetings.CreateMeeting;

/// <summary>
/// Комманда создания мероприятий
/// </summary>
/// <param name="Meeting"></param>
public record CreateMeetingCommand(MeetingRequestDto Meeting) : IRequest<MeetingResponseDto>;