using MediatR;

namespace SenseCapitalTraineeTask.Features.Images.ImageGuids;

/// <summary>
/// Запрос на получение картинок
/// </summary>
public record GetImgGuidsQuery : IRequest<ImgGuidsResponseDto>; 