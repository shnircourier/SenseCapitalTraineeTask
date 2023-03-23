using MediatR;

namespace SenseCapitalTraineeTask.Features.Meetings.DeleteMeeting;

/// <summary>
/// Комманда на удаление мероприятия
/// </summary>
/// <param name="Id"></param>
public record DeleteMeetingCommand(string Id) : IRequest<MeetingResponseDto>;