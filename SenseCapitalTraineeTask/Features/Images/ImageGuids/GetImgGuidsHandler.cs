using JetBrains.Annotations;
using MediatR;
using SenseCapitalTraineeTask.Data;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Images.ImageGuids;

/// <summary>
/// Запрос на получение списка Id Картинок
/// </summary>
[UsedImplicitly]
public class GetImgGuidsHandler : IRequestHandler<GetImgGuidsQuery, ImgGuidsResponseDto>
{
    private readonly IRepository<Image> _repository;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repository">БД</param>
    public GetImgGuidsHandler(IRepository<Image> repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<ImgGuidsResponseDto> Handle(GetImgGuidsQuery request, CancellationToken cancellationToken)
    {
        var response = await _repository.Get();

        var images = response.Select(i => i.Id).ToHashSet();
        
        return new ImgGuidsResponseDto(images);
    }
}