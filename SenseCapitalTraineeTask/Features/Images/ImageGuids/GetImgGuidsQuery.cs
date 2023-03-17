using MediatR;

namespace SenseCapitalTraineeTask.Features.Images.ImageGuids;

public record GetImgGuidsQuery : IRequest<ImgGuidsResponseDto>; 