using MediatR;
using SenseCapitalTraineeTask.Images.Data;
using SenseCapitalTraineeTask.Images.Data.Entities;

namespace SenseCapitalTraineeTask.Images.Features.ImageById;

public class ImageByIdHandler : IRequestHandler<ImageByIdQuery, string?>
{
    private readonly IRepository<Image> _repository;

    public ImageByIdHandler(IRepository<Image> repository)
    {
        _repository = repository;
    }
    
    public async Task<string?> Handle(ImageByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.Get(request.Id);
        
        // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
        return result?.Id;
    }
}