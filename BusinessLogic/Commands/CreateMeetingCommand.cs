using BusinessLogic.Models;
using MediatR;

namespace BusinessLogic.Commands;

public record CreateMeetingCommand(MeetingRequest Meeting) : IRequest<MeetingResponse>;