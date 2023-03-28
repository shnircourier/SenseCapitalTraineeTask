using JetBrains.Annotations;
using MediatR;

namespace SenseCapitalTraineeTask.Images.Features.ImageList;

[UsedImplicitly]
public record ImageListRequest : IRequest<List<string>>;