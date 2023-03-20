using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;
using SenseCapitalTraineeTask.Features.Images.ImageGuids;

namespace SenseCapitalTraineeTask.Features.Images;

/// <summary>
/// Контроллер картинок
/// </summary>
[ApiController]
[Authorize]
[Route("images")]
public class ImagesController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <inheritdoc />
    public ImagesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Получение хеш-множества id
    /// </summary>
    /// <returns>Множество id</returns>
    [HttpGet]
    public async Task<ScResult<ImgGuidsResponseDto>> GetImgGuids()
    {
        var response = await _mediator.Send(new GetImgGuidsQuery());

        return new ScResult<ImgGuidsResponseDto>(response);
    }
}