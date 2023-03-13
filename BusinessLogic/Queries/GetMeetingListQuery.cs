using BusinessLogic.Models;
using MediatR;

namespace BusinessLogic.Queries;

public record GetMeetingListQuery() : IRequest<List<MeetingResponse>>;