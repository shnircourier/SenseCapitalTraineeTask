using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.ScResult;

namespace SenseCapitalTraineeTask.Features.Images.ImageById;

/// <summary>
/// Запрос на получение картинки по id
/// </summary>
/// <param name="Id"></param>
[UsedImplicitly]
public record ImageByIdRequest(string Id) : IRequest<ScResult<string>>;