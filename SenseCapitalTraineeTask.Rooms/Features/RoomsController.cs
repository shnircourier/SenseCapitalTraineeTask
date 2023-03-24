using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;
using SenseCapitalTraineeTask.Rooms.Features.RoomById;
using SenseCapitalTraineeTask.Rooms.Features.RoomList;
using SenseCapitalTraineeTask.Rooms.Infrastructure;

namespace SenseCapitalTraineeTask.Rooms.Features;

[Authorize]
[ApiController]
[Route("rooms")]
public class RoomsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly RabbitMqSenderService _service;

    public RoomsController(IMediator mediator, RabbitMqSenderService service)
    {
        _mediator = mediator;
        _service = service;
    }

    [HttpGet]
    public async Task<ScResult<List<string>>> Get()
    {
        var response = await _mediator.Send(new RoomListQuery());

        return new ScResult<List<string>>(response);
    }

    [HttpGet("{id}")]
    public async Task<ScResult<string>> Get(string id)
    {
        var response = await _mediator.Send(new RoomByIdQuery(id));

        return new ScResult<string>(response);
    }

    [HttpGet("test/{id}")]
    public ActionResult TestPathForSendingMessage(string id)
    {
        var eventBody = new EventBody
        {
            DeletedId = id,
            EventType = EventType.SpaceDeleteEvent,
            QueueName = "SpaceDeleteEvent"
        };
        
        _service.SendingMessage(eventBody);
        
        return Ok();
    }
}