using JetBrains.Annotations;
using MediatR;

namespace SenseCapitalTraineeTask.Features.Images.ImageById;

[UsedImplicitly]
public record ImageByIdQuery(string Id) : IRequest<string>;