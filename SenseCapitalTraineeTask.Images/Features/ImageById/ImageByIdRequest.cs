using JetBrains.Annotations;
using MediatR;

namespace SenseCapitalTraineeTask.Images.Features.ImageById;

[UsedImplicitly]
public record ImageByIdRequest(string Id) : IRequest<string>;