using JetBrains.Annotations;
using MediatR;

namespace SenseCapitalTraineeTask.Rooms.Features.RoomById;

[UsedImplicitly]
public record RoomByIdQuery(string Id) : IRequest<string>; 