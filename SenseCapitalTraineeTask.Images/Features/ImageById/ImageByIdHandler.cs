using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.Exceptions;
using SenseCapitalTraineeTask.Images.Features.Data;
using SenseCapitalTraineeTask.Images.Features.Data.Entities;

namespace SenseCapitalTraineeTask.Images.Features.ImageById;

[UsedImplicitly]
public class ImageByIdHandler : IRequestHandler<ImageByIdRequest, string>
{
    private readonly IRepository<Image> _repository;

    public ImageByIdHandler(IRepository<Image> repository)
    {
        _repository = repository;
    }
    
    public async Task<string> Handle(ImageByIdRequest request, CancellationToken cancellationToken)
    {
        var result = await _repository.Get(request.Id);

        if (result is null)
        {
            throw new ScException("Картинка не найдена");
        }

        return result.Id!;
    }
}