using MediatR;

namespace SenseCapitalTraineeTask.Features.Meetings.UpdateMeeting;

/// <summary>
/// Команда на обновление данных мероприятия
/// </summary>
/// <param name="Meeting">Модель мероприятия</param>
/// <param name="Id">Id мероприятия</param>
public record UpdateMeetingRequest(MeetingRequestDto Meeting, string Id) : IRequest<MeetingResponseDto>;