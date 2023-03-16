using MediatR;
using Microsoft.AspNetCore.Mvc;
using SenseCapitalTraineeTask.Features.Rooms.RoomGuids;

namespace SenseCapitalTraineeTask.Features.Rooms;

[ApiController]
[Route("rooms")]
public class RoomsController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoomsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<RoomGuidsResponse>> GetRoomGuids()
    {
        var response = await _mediator.Send(new GetRoomGuidsQuery());

        return Ok(response);
    }
}