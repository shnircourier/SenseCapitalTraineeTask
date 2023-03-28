using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;
using SenseCapitalTraineeTask.Rooms.Features.RoomById;
using SenseCapitalTraineeTask.Rooms.Features.RoomList;

namespace SenseCapitalTraineeTask.Rooms.Features;

[Authorize]
[ApiController]
[Route("rooms")]
public class RoomsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly RoomsSenderService _service;
    private readonly ILogger<RoomsController> _logger;

    public RoomsController(IMediator mediator, RoomsSenderService service, ILogger<RoomsController> logger)
    {
        _mediator = mediator;
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ScResult<List<string>>> Get()
    {
        var response = await _mediator.Send(new RoomListRequest());

        return new ScResult<List<string>>(response);
    }

    [HttpGet("{id}")]
    public async Task<ScResult<string>> Get(string id)
    {
        _logger.LogInformation("Запрос: {0}", id);
        
        var response = await _mediator.Send(new RoomByIdRequest(id));

        _logger.LogInformation("Ответ: {0}", response);
        
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