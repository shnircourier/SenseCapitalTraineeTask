using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;
using SenseCapitalTraineeTask.Features.Auth.GetToken;
using SenseCapitalTraineeTask.Features.Auth.GetUsers;

namespace SenseCapitalTraineeTask.Features.Auth;

/// <summary>
/// Контроллер авторизации
/// </summary>
[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AuthController> _logger;

    /// <summary>
    /// Контроллер авторизации
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="logger"></param>
    public AuthController(IMediator mediator, ILogger<AuthController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Получить список заготовленных пользователей
    /// </summary>
    /// <returns>Список заготовленных пользователей</returns>
    [HttpGet("get-users")]
    public async Task<ScResult<List<UserResponseDto>>> GetUsers()
    {
        var response = await _mediator.Send(new GetUsersQuery());
        
        _logger.LogInformation("Ответ: {0}", response);

        return new ScResult<List<UserResponseDto>>(response);
    }

    
    /// <summary>
    /// Получить JWT
    /// </summary>
    /// <param name="userRequestDto">Модель пользователя</param>
    /// <returns>JWT в виде строки</returns>
    /// <response code="400">Неправильное имя пользователя или пароль</response>
    /// <response code="400">Ошибка обращения к IdentityServer</response>
    [HttpPost("get-token")]
    public async Task<ScResult<string>> GetToken([FromBody] UserRequestDto userRequestDto)
    {
        _logger.LogInformation("Запрос: {0}", userRequestDto);
        
        var response = await _mediator.Send(new GetTokenRequest(userRequestDto));

        _logger.LogInformation("Ответ: {0}", response);
        
        return new ScResult<string>(response);
    }

    /// <summary>
    /// Тестовый закрытый путь из 2-го задания
    /// </summary>
    /// <response code="200">Ок</response>
    /// <response code="401">Запрещено</response>
    [Authorize]
    // ReSharper disable once StringLiteralTypo
    [HttpGet("stub/authstub")]
    public ActionResult CheckToken()
    {
        _logger.LogInformation("Запрос на тестовый маршрут для проверки авторизации");
        return Ok();
    }
}