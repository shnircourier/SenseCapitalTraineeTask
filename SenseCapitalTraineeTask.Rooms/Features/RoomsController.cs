using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SenseCapitalTraineeTask.Rooms.Features.RoomById;
using SenseCapitalTraineeTask.Rooms.Features.RoomList;

namespace SenseCapitalTraineeTask.Rooms.Features;

[Authorize]
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
    public async Task<List<string>> Get()
    {
        var response = await _mediator.Send(new RoomListQuery());

        return response;
    }

    [HttpGet("{id}")]
    public async Task<string?> Get(string id)
    {
        var response = await _mediator.Send(new RoomByIdQuery(id));

        return response;
    }
}