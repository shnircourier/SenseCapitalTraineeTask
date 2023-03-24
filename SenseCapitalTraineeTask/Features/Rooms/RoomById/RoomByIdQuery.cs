using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.ScResult;

namespace SenseCapitalTraineeTask.Features.Rooms.RoomById;

[UsedImplicitly]
public record RoomByIdQuery(string Id) : IRequest<ScResult<string>>;