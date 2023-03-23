using JetBrains.Annotations;
using MediatR;

namespace SenseCapitalTraineeTask.Rooms.Features.RoomList;

[UsedImplicitly]
public record RoomListQuery() : IRequest<List<string>>;