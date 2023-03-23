using MediatR;

namespace SenseCapitalTraineeTask.Features.Meetings.MeetingList;

/// <summary>
/// Запрос на получение списка мероприятий
/// </summary>
public record GetMeetingListQuery : IRequest<List<MeetingResponseDto>>;