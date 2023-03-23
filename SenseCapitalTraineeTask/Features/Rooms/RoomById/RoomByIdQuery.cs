using JetBrains.Annotations;
using MediatR;

namespace SenseCapitalTraineeTask.Features.Rooms.RoomById;

[UsedImplicitly]
public record RoomByIdQuery(string Id) : IRequest<string>;