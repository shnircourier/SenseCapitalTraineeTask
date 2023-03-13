using BusinessLogic.Models;
using MediatR;

namespace BusinessLogic.Commands;

public record DeleteMeetingCommand(Guid Id) : IRequest<MeetingResponse>;