using MediatR;
using SenseCapitalTraineeTask.Images.Data;
using SenseCapitalTraineeTask.Images.Data.Entities;

namespace SenseCapitalTraineeTask.Images.Features.ImageList;

public class ImageListHandler : IRequestHandler<ImageListQuery, List<string>>
{
    private readonly IRepository<Image> _repository;

    public ImageListHandler(IRepository<Image> repository)
    {
        _repository = repository;
    }
    
    public async Task<List<string>> Handle(ImageListQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.Get();

        return result.Select(r => r.Id).ToList();
    }
}