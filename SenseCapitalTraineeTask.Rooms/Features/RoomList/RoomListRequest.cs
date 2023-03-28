using JetBrains.Annotations;
using MediatR;

namespace SenseCapitalTraineeTask.Rooms.Features.RoomList;

[UsedImplicitly]
public record RoomListRequest : IRequest<List<string>>;