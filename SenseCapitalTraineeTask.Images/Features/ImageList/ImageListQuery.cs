using JetBrains.Annotations;
using MediatR;

namespace SenseCapitalTraineeTask.Images.Features.ImageList;

[UsedImplicitly]
public record ImageListQuery : IRequest<List<string>>;