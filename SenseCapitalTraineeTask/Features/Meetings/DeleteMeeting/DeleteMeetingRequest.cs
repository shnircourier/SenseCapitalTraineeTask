using MediatR;

namespace SenseCapitalTraineeTask.Features.Meetings.DeleteMeeting;

/// <summary>
/// Команда на удаление мероприятия
/// </summary>
/// <param name="Id"></param>
public record DeleteMeetingRequest(string Id) : IRequest<MeetingResponseDto>;