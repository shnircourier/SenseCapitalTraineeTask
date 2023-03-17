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
    private readonly IRepository<Meeting> _repository;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="repository">БД</param>
    public GetImgGuidsHandler(IRepository<Meeting> repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public Task<ImgGuidsResponseDto> Handle(GetImgGuidsQuery request, CancellationToken cancellationToken)
    {
        var response = _repository.GetAvailableImgGuids();
        
        return Task.FromResult(new ImgGuidsResponseDto(response));
    }
}