using MediatR;

namespace SenseCapitalTraineeTask.Features.Rooms.RoomGuids;

public record GetRoomGuidsQuery : IRequest<RoomGuidsResponseDto>;