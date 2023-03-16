using MediatR;

namespace SenseCapitalTraineeTask.Features.Meetings.MeetingList;

public record GetMeetingListQuery() : IRequest<List<MeetingResponse>>;