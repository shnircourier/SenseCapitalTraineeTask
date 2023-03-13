using BusinessLogic.Models;
using MediatR;

namespace BusinessLogic.Queries;

public record GetRoomGuidsQuery() : IRequest<RoomGuidsResponse>;