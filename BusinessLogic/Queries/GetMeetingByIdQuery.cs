using BusinessLogic.Models;
using MediatR;

namespace BusinessLogic.Queries;

public record GetMeetingByIdQuery(Guid Id) : IRequest<MeetingResponse>;