using MediatR;
using Microsoft.AspNetCore.Mvc;
using SenseCapitalTraineeTask.Features.Images.ImageGuids;

namespace SenseCapitalTraineeTask.Features.Images;

[ApiController]
[Route("images")]
public class ImagesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ImagesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<ImgGuidsResponse>> GetImgGuids()
    {
        var response = await _mediator.Send(new GetImgGuidsQuery());

        return Ok(response);
    }
}