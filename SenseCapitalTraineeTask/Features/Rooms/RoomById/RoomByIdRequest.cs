using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.ScResult;

namespace SenseCapitalTraineeTask.Features.Rooms.RoomById;

/// <summary>
/// Запрос получения помещения по Id
/// </summary>
/// <param name="Id"></param>
[UsedImplicitly]
public record RoomByIdRequest(string Id) : IRequest<ScResult<string>>;