using MediatR;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;
using SenseCapitalTraineeTask.Features.Meetings.CreateFreeTickets;
using SenseCapitalTraineeTask.Features.Meetings.CreateMeeting;
using SenseCapitalTraineeTask.Features.Meetings.DeleteMeeting;
using SenseCapitalTraineeTask.Features.Meetings.GiveTicketToUser;
using SenseCapitalTraineeTask.Features.Meetings.MeetingById;
using SenseCapitalTraineeTask.Features.Meetings.MeetingList;
using SenseCapitalTraineeTask.Features.Meetings.UpdateMeeting;

namespace SenseCapitalTraineeTask.Features.Meetings;

/// <summary>
/// Контроллер мероприятий
/// </summary>
[ApiController]
[Route("meetings")]
public class MeetingsController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <inheritdoc />
    public MeetingsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Получение всех мероприятий
    /// </summary>
    /// <returns>Список мероприятий</returns>
    [HttpGet]
    public async Task<ScResult<List<MeetingResponseDto>>> Get()
    {
        var response = await _mediator.Send(new GetMeetingListQuery());
        
        return new ScResult<List<MeetingResponseDto>>(response);
    }

    /// <summary>
    /// Получение мероприятия по Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Модель мероприятия</returns>
    /// <response code="200">Модель мероприятия</response>
    /// <response code="400">Модель не найдена</response>
    [HttpGet("{id:guid}")]
    public async Task<ScResult<MeetingResponseDto>> Get([FromRoute] Guid id)
    {
        var response = await _mediator.Send(new GetMeetingByIdQuery(id));

        return new ScResult<MeetingResponseDto>(response);
    }

    /// <summary>
    /// Создание мероприятия
    /// </summary>
    /// <param name="requestDto">Тело запроса</param>
    /// <returns>Созданая модель</returns>
    /// <response code="200">Модель мероприятия</response>
    /// <response code="422">Ошибка валидации</response>
    [HttpPost]
    public async Task<ScResult<MeetingResponseDto>> Create([FromBody] MeetingRequestDto requestDto)
    {
        var response = await _mediator.Send(new CreateMeetingCommand(requestDto));

        return new ScResult<MeetingResponseDto>(response);
    }

    /// <summary>
    /// Обновление мероприятия по Id
    /// </summary>
    /// <param name="requestDto"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200">Модель мероприятия</response>
    /// <response code="400">Модель не найдена</response>
    /// <response code="422">Ошибка валидации</response>
    [HttpPut("{id:guid}")]
    public async Task<ScResult<MeetingResponseDto>> Update([FromBody] MeetingRequestDto requestDto, [FromRoute] Guid id)
    {
        var response = await _mediator.Send(new UpdateMeetingCommand(requestDto, id));

        return new ScResult<MeetingResponseDto>(response);
    }

    /// <summary>
    /// Удаление мероприятия по Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    public async Task<ScResult<MeetingResponseDto>> Delete([FromRoute] Guid id)
    {
        var response = await _mediator.Send(new DeleteMeetingCommand(id));

        return new ScResult<MeetingResponseDto>(response);
    }

    /// <summary>
    /// Создать билеты на мероприятие
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    /// <response code="200">Модель мероприятия</response>
    /// <response code="422">Ошибка валидации</response>
    [HttpPost("tickets/create")]
    public async Task<ScResult<MeetingResponseDto>> CreateFreeTickets([FromBody] CreateFreeTicketsRequestDto requestDto)
    {
        var response = await _mediator.Send(new CreateFreeTicketsCommand(requestDto));

        return new ScResult<MeetingResponseDto>(response);
    }

    /// <summary>
    /// Выдать пользователю билет
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    /// <response code="200">Модель мероприятия</response>
    /// <response code="400">Билеты закончились</response>
    [HttpPost("tickets/user")]
    public async Task<ScResult<MeetingResponseDto>> GiveTicketToUser([FromBody] TicketRequestDto requestDto)
    {
        var response = await _mediator.Send(new GiveTicketToUserCommand(requestDto));

        return new ScResult<MeetingResponseDto>(response);
    }
}
