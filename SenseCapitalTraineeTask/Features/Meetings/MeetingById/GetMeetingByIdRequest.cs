using MediatR;

namespace SenseCapitalTraineeTask.Features.Meetings.MeetingById;

/// <summary>
/// Запрос на получение мероприятия по Id
/// </summary>
/// <param name="Id">Id Мероприятия</param>
public record GetMeetingByIdRequest(string Id) : IRequest<MeetingResponseDto>;