using MediatR;

namespace SenseCapitalTraineeTask.Features.Meetings.MeetingList;

/// <summary>
/// Запрос на получение списка мероприятий
/// </summary>
public record GetMeetingListRequest : IRequest<List<MeetingResponseDto>>;