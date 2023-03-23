using JetBrains.Annotations;
using MediatR;

namespace SenseCapitalTraineeTask.Images.Features.ImageById;

[UsedImplicitly]
public record ImageByIdQuery(string Id) : IRequest<string?>;