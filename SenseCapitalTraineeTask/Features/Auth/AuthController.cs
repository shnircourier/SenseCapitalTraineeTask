using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;
using SenseCapitalTraineeTask.Features.Auth.GetToken;

namespace SenseCapitalTraineeTask.Features.Auth;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("get-token")]
    public async Task<ScResult<string>> GetToken()
    {
        var response = await _mediator.Send(new GetTokenQuery());

        return new ScResult<string>(response);
    }

    [Authorize]
    [HttpGet("stub/authstub")]
    public ActionResult CheckToken()
    {
        return Ok();
    }
}