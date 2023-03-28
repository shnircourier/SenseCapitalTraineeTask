using JetBrains.Annotations;
using MediatR;
using SenseCapitalTraineeTask.Images.Features.Data;
using SenseCapitalTraineeTask.Images.Features.Data.Entities;

namespace SenseCapitalTraineeTask.Images.Features.ImageList;

[UsedImplicitly]
public class ImageListHandler : IRequestHandler<ImageListRequest, List<string>>
{
    private readonly IRepository<Image> _repository;

    public ImageListHandler(IRepository<Image> repository)
    {
        _repository = repository;
    }
    
    public async Task<List<string>> Handle(ImageListRequest request, CancellationToken cancellationToken)
    {
        var result = await _repository.Get();

        return result.Select(r => r.Id).ToList()!;
    }
}