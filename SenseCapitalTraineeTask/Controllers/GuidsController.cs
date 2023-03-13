using BusinessLogic.Models;
using BusinessLogic.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SenseCapitalTraineeTask.Controllers;

[ApiController]
[Route("guids")]
public class GuidsController : ControllerBase
{
    private readonly IMediator _mediator;

    public GuidsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [Route("imgGuids")]
    public async Task<ActionResult<ImgGuidsResponse>> GetImgGuids()
    {
        var response = await _mediator.Send(new GetImgGuidsQuery());

        return Ok(response);
    }

    [HttpGet]
    [Route("roomGuids")]
    public async Task<ActionResult<RoomGuidsResponse>> GetRoomGuids()
    {
        var response = await _mediator.Send(new GetRoomGuidsQuery());

        return Ok(response);
    }
}