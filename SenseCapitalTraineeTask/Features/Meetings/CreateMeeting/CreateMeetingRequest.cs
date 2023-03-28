using MediatR;

namespace SenseCapitalTraineeTask.Features.Meetings.CreateMeeting;

/// <summary>
/// Команда создания мероприятий
/// </summary>
/// <param name="Meeting"></param>
public record CreateMeetingRequest(MeetingRequestDto Meeting) : IRequest<MeetingResponseDto>;