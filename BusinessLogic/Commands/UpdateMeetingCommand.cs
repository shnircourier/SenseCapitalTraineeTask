using BusinessLogic.Models;
using MediatR;

namespace BusinessLogic.Commands;

public record UpdateMeetingCommand(MeetingRequest Meeting, Guid Id) : IRequest<MeetingResponse>;