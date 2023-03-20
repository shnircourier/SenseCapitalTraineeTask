using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;
using SenseCapitalTraineeTask.Features.Auth.GetToken;
using SenseCapitalTraineeTask.Features.Auth.GetUsers;

namespace SenseCapitalTraineeTask.Features.Auth;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Контроллер авторизации
    /// </summary>
    /// <param name="mediator"></param>
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получить список заготовленных пользователей
    /// </summary>
    /// <returns>Список заготовленных пользователей</returns>
    [HttpGet("get-users")]
    public async Task<ScResult<List<UserResponseDto>>> GetUsers()
    {
        var response = await _mediator.Send(new GetUsersQuery());

        return new ScResult<List<UserResponseDto>>(response);
    }

    
    /// <summary>
    /// Получить JWT токен
    /// </summary>
    /// <param name="userRequestDto">Модель пользователя</param>
    /// <returns>JWT токен в виде строки</returns>
    /// <response code="400">Неправильный логин или пароль</response>
    /// <response code="400">Ошибка обращения к IdentityServer</response>
    [HttpPost("get-token")]
    public async Task<ScResult<string>> GetToken([FromBody] UserRequestDto userRequestDto)
    {
        var response = await _mediator.Send(new GetTokenQuery(userRequestDto));

        return new ScResult<string>(response);
    }

    /// <summary>
    /// Тестовый закрытый путь из 2-го задания
    /// </summary>
    /// <response code="200">Ок</response>
    /// <response code="401">Запрещено</response>
    [Authorize]
    [HttpGet("stub/authstub")]
    public ActionResult CheckToken()
    {
        return Ok();
    }
}