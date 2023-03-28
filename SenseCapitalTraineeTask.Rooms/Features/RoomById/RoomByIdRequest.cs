using JetBrains.Annotations;
using MediatR;

namespace SenseCapitalTraineeTask.Rooms.Features.RoomById;

[UsedImplicitly]
public record RoomByIdRequest(string Id) : IRequest<string>; 