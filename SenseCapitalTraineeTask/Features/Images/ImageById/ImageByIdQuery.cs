using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.ScResult;

namespace SenseCapitalTraineeTask.Features.Images.ImageById;

[UsedImplicitly]
public record ImageByIdQuery(string Id) : IRequest<ScResult<string>>;