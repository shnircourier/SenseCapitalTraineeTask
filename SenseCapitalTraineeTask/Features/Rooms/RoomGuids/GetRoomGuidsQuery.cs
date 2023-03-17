using MediatR;

namespace SenseCapitalTraineeTask.Features.Rooms.RoomGuids;

/// <summary>
/// Запрос на получения множества id
/// </summary>
public record GetRoomGuidsQuery : IRequest<RoomGuidsResponseDto>;