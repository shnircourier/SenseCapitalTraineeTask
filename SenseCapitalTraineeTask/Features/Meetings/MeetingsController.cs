using MediatR;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;
using SenseCapitalTraineeTask.Features.Meetings.CreateFreeTickets;
using SenseCapitalTraineeTask.Features.Meetings.CreateMeeting;
using SenseCapitalTraineeTask.Features.Meetings.DeleteMeeting;
using SenseCapitalTraineeTask.Features.Meetings.MeetingById;
using SenseCapitalTraineeTask.Features.Meetings.MeetingList;
using SenseCapitalTraineeTask.Features.Meetings.UpdateMeeting;

namespace SenseCapitalTraineeTask.Features.Meetings;

[ApiController]
[Route("meetings")]
public class MeetingsController : ControllerBase
{
    private readonly IMediator _mediator;

    public MeetingsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ScResult<List<MeetingResponseDto>>> Get()
    {
        var response = await _mediator.Send(new GetMeetingListQuery());
        
        return new ScResult<List<MeetingResponseDto>>(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ScResult<MeetingResponseDto>> Get([FromRoute] Guid id)
    {
        var response = await _mediator.Send(new GetMeetingByIdQuery(id));

        return new ScResult<MeetingResponseDto>(response);
    }

    [HttpPost]
    public async Task<ScResult<MeetingResponseDto>> Create([FromBody] MeetingRequestDto requestDto)
    {
        var response = await _mediator.Send(new CreateMeetingCommand(requestDto));

        return new ScResult<MeetingResponseDto>(response);
    }

    [HttpPut("{id:guid}")]
    public async Task<ScResult<MeetingResponseDto>> Update([FromBody] MeetingRequestDto requestDto, [FromRoute] Guid id)
    {
        var response = await _mediator.Send(new UpdateMeetingCommand(requestDto, id));

        return new ScResult<MeetingResponseDto>(response);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ScResult<MeetingResponseDto>> Delete([FromRoute] Guid id)
    {
        var response = await _mediator.Send(new DeleteMeetingCommand(id));

        return new ScResult<MeetingResponseDto>(response);
    }

    [HttpPost("tickets/create")]
    public async Task<ScResult<MeetingResponseDto>> CreateFreeTickets([FromBody] CreateFreeTicketsRequestDto requestDto)
    {
        var response = await _mediator.Send(new CreateFreeTicketsCommand(requestDto));

        return new ScResult<MeetingResponseDto>(response);
    }
}
