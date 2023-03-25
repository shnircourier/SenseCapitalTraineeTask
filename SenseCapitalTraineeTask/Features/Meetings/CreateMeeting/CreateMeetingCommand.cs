using MediatR;

namespace SenseCapitalTraineeTask.Features.Meetings.CreateMeeting;

/// <summary>
/// Команда создания мероприятий
/// </summary>
/// <param name="Meeting"></param>
public record CreateMeetingCommand(MeetingRequestDto Meeting) : IRequest<MeetingResponseDto>;