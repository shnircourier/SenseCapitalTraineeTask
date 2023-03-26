using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;
using SenseCapitalTraineeTask.Payment.Features.Payment.CreatePayment;
using SenseCapitalTraineeTask.Payment.Features.Payment.Data;
using SenseCapitalTraineeTask.Payment.Features.Payment.PaymentList;
using SenseCapitalTraineeTask.Payment.Features.Payment.UpdatePaymentStatus;

namespace SenseCapitalTraineeTask.Payment.Features.Payment;

[Authorize]
[ApiController]
[Route("payments")]
public class PaymentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ScResult<List<PaymentOperation>>> Get()
    {
        var response = await _mediator.Send(new PaymentListQuery());

        return new ScResult<List<PaymentOperation>>(response);
    }

    [HttpPost("")]
    public async Task<ScResult<PaymentOperation>> Create([FromBody] PaymentCreateRequest request)
    {
        var response = await _mediator.Send(new CreatePaymentCommand(request.Description));

        return new ScResult<PaymentOperation>(response);
    }

    [HttpPatch("status")]
    public async Task<ScResult<PaymentOperation>> UpdateStatus([FromBody] UpdatePaymentStatusRequest request)
    {
        var response = await _mediator.Send(new UpdatePaymentStatusCommand(request));

        return new ScResult<PaymentOperation>(response);
    }
}