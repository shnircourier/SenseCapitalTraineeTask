using BusinessLogic.Commands;
using BusinessLogic.Models;
using BusinessLogic.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SenseCapitalTraineeTask.Controllers;

[ApiController]
[Route("meeting")]
public class MeetingsController : ControllerBase
{
    private readonly IMediator _mediator;

    public MeetingsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<MeetingResponse>>> Get()
    {
        var response = await _mediator.Send(new GetMeetingListQuery());
        
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MeetingResponse>> Get([FromRoute] Guid id)
    {
        var response = await _mediator.Send(new GetMeetingByIdQuery(id));

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<MeetingResponse>> Create([FromBody] MeetingRequest request)
    {
        var response = await _mediator.Send(new CreateMeetingCommand(request));

        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<MeetingResponse>> Update([FromBody] MeetingRequest request, [FromRoute] Guid id)
    {
        var response = await _mediator.Send(new UpdateMeetingCommand(request, id));

        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<MeetingResponse>> Delete([FromRoute] Guid id)
    {
        var response = await _mediator.Send(new DeleteMeetingCommand(id));

        return Ok(response);
    }
}