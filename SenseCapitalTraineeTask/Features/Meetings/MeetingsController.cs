using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;
using SenseCapitalTraineeTask.Features.Meetings.CheckUserTicket;
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
[Authorize]
public class MeetingsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<MeetingsController> _logger;

    /// <inheritdoc />
    public MeetingsController(IMediator mediator, ILogger<MeetingsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    
    /// <summary>
    /// Получение всех мероприятий
    /// </summary>
    /// <returns>Список мероприятий</returns>
    [HttpGet]
    public async Task<ScResult<List<MeetingResponseDto>>> Get()
    {
        var response = await _mediator.Send(new GetMeetingListQuery());
        
        _logger.LogInformation("Ответ: {0}", response);
        
        return new ScResult<List<MeetingResponseDto>>(response);
    }

    /// <summary>
    /// Получение мероприятия по Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Модель мероприятия</returns>
    /// <response code="200">Модель мероприятия</response>
    /// <response code="400">Модель не найдена</response>
    // ReSharper disable once RouteTemplates.ActionRoutePrefixCanBeExtractedToControllerRoute
    [HttpGet("{id}")]
    public async Task<ScResult<MeetingResponseDto>> Get([FromRoute] string id)
    {
        _logger.LogInformation("Запрос: {0}", id);
        
        var response = await _mediator.Send(new GetMeetingByIdQuery(id));
        
        _logger.LogInformation("Ответ: {0}", response);

        return new ScResult<MeetingResponseDto>(response);
    }

    /// <summary>
    /// Создание мероприятия
    /// </summary>
    /// <param name="requestDto">Тело запроса</param>
    /// <returns>Созданная модель</returns>
    /// <response code="200">Модель мероприятия</response>
    /// <response code="422">Ошибка валидации</response>
    [HttpPost]
    public async Task<ScResult<MeetingResponseDto>> Create([FromBody] MeetingRequestDto requestDto)
    {
        _logger.LogInformation("Запрос: {0}", requestDto);
        
        var response = await _mediator.Send(new CreateMeetingCommand(requestDto));
        
        _logger.LogInformation("Ответ: {0}", response);

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
    // ReSharper disable once RouteTemplates.ActionRoutePrefixCanBeExtractedToControllerRoute
    [HttpPut("{id}")]
    public async Task<ScResult<MeetingResponseDto>> Update([FromBody] MeetingRequestDto requestDto, [FromRoute] string id)
    {
        _logger.LogInformation("Запрос: [FromBody] {0}; [FromRoute] {1}", requestDto, id);
        
        var response = await _mediator.Send(new UpdateMeetingCommand(requestDto, id));
        
        _logger.LogInformation("Ответ: {0}", response);

        return new ScResult<MeetingResponseDto>(response);
    }

    /// <summary>
    /// Удаление мероприятия по Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // ReSharper disable once RouteTemplates.ActionRoutePrefixCanBeExtractedToControllerRoute
    [HttpDelete("{id}")]
    public async Task<ScResult<MeetingResponseDto>> Delete([FromRoute] string id)
    {
        _logger.LogInformation("Запрос: {0}", id);
        
        var response = await _mediator.Send(new DeleteMeetingCommand(id));

        _logger.LogInformation("Ответ: {0}", response);
        
        return new ScResult<MeetingResponseDto>(response);
    }

    /// <summary>
    /// Создать билеты на мероприятие
    /// </summary>
    /// <param name="requestDto"></param>
    /// <param name="id">Идентификатор мероприятия</param>
    /// <returns></returns>
    /// <response code="200">Модель мероприятия</response>
    /// <response code="422">Ошибка валидации</response>
    // ReSharper disable once RouteTemplates.ActionRoutePrefixCanBeExtractedToControllerRoute
    [HttpPost("{id}/tickets/create")]
    public async Task<ScResult<MeetingResponseDto>> CreateFreeTickets([FromBody] CreateFreeTicketsRequestDto requestDto, [FromRoute] string id)
    {
        _logger.LogInformation("Запрос: [FromBody] {0}; [FromRoute] {1}", requestDto, id);
        
        var response = await _mediator.Send(new CreateFreeTicketsCommand(requestDto, id));
        
        _logger.LogInformation("Ответ: {0}", response);

        return new ScResult<MeetingResponseDto>(response);
    }

    /// <summary>
    /// Выдать пользователю билет
    /// </summary>
    /// <param name="requestDto"></param>
    /// <param name="id">Идентификатор мероприятия</param>
    /// <returns></returns>
    /// <response code="200">Модель мероприятия</response>
    /// <response code="400">Билеты закончились</response>
    // ReSharper disable once RouteTemplates.ActionRoutePrefixCanBeExtractedToControllerRoute
    [HttpPost("{id}/tickets/user")]
    public async Task<ScResult<MeetingResponseDto>> GiveTicketToUser([FromBody] TicketRequestDto requestDto, [FromRoute] string id)
    {
        _logger.LogInformation("Запрос: [FromBody] {0}; [FromRoute] {1}", requestDto, id);
        
        var response = await _mediator.Send(new GiveTicketToUserCommand(requestDto, id));
        
        _logger.LogInformation("Ответ: {0}", response);

        return new ScResult<MeetingResponseDto>(response);
    }

    /// <summary>
    /// Проверить билет мероприятия
    /// </summary>
    /// <param name="requestDto">Модель запроса</param>
    /// <param name="id">Идентификатор мероприятия</param>
    /// <returns></returns>
    /// <response code="400">Мероприятие не найдено</response>
    /// <response code="400">Билет не найден</response>
    /// <response code="400">Билет не принадлежит пользователю</response>
    /// <response code="400">Места не совпадают</response>
    // ReSharper disable once RouteTemplates.ActionRoutePrefixCanBeExtractedToControllerRoute
    [HttpPost("{id}/ticket/check")]
    public async Task<ScResult> CheckTicket([FromBody] CheckTicketRequestDto requestDto, [FromRoute] string id)
    {
        _logger.LogInformation("Запрос: [FromBody] {0}; [FromRoute] {1}", requestDto, id);
        
        await _mediator.Send(new CheckUserTicketQuery(id, requestDto));

        return new ScResult();
    }
}
