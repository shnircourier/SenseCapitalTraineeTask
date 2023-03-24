using MediatR;
using SC.Internship.Common.Exceptions;
using SenseCapitalTraineeTask.Images.Data;
using SenseCapitalTraineeTask.Images.Data.Entities;

namespace SenseCapitalTraineeTask.Images.Features.ImageById;

public class ImageByIdHandler : IRequestHandler<ImageByIdQuery, string>
{
    private readonly IRepository<Image> _repository;

    public ImageByIdHandler(IRepository<Image> repository)
    {
        _repository = repository;
    }
    
    public async Task<string> Handle(ImageByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.Get(request.Id);

        if (result is null)
        {
            throw new ScException("Картинка не найдена");
        }

        return result.Id!;
    }
}