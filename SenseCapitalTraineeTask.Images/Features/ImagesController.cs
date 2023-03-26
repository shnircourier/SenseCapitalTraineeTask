using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;
using SenseCapitalTraineeTask.Images.Features.ImageById;
using SenseCapitalTraineeTask.Images.Features.ImageList;
using SenseCapitalTraineeTask.Images.Infrastructure;

namespace SenseCapitalTraineeTask.Images.Features;

[Authorize]
[ApiController]
[Route("images")]
public class ImagesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ImageSenderService _service;
    private readonly ILogger<ImagesController> _logger;

    public ImagesController(IMediator mediator, ImageSenderService service, ILogger<ImagesController> logger)
    {
        _mediator = mediator;
        _service = service;
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<ScResult<List<string>>> Get()
    {
        var response = await _mediator.Send(new ImageListQuery());

        return new ScResult<List<string>>(response);
    }

    [HttpGet("{id}")]
    public async Task<ScResult<string>> Get(string id)
    {
        _logger.LogInformation("Запрос: {0}", id);
        
        var response = await _mediator.Send(new ImageByIdQuery(id));
        
        _logger.LogInformation("Ответ: {0}", response);

        return new ScResult<string>(response);
    }
    
    [HttpGet("test/{id}")]
    public ActionResult TestPathForSendingMessage(string id)
    {
        var eventBody = new EventBody
        {
            DeletedId = id,
            EventType = EventType.ImageDeleteEvent,
            QueueName = "ImageDeleteEvent"
        };
        
        _service.SendingMessage(eventBody);
        
        return Ok();
    }
}