using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;
using SenseCapitalTraineeTask.Payment.Data;
using SenseCapitalTraineeTask.Payment.Features.Payment.CreatePayment;
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

    [HttpPost]
    public async Task<ScResult<PaymentOperation>> Create([FromBody] string description)
    {
        var response = await _mediator.Send(new CreatePaymentCommand(description));

        return new ScResult<PaymentOperation>(response);
    }

    [HttpPatch("confirm")]
    public async Task<ScResult<PaymentOperation>> ConfirmPayment([FromBody] UpdatePaymentStatusRequest request)
    {
        var response = await _mediator.Send(new UpdatePaymentStatusCommand(request));

        return new ScResult<PaymentOperation>(response);
    }

    [HttpPatch("cancel")]
    public async Task<ScResult<PaymentOperation>> CancelPayment([FromBody] UpdatePaymentStatusRequest request)
    {
        var response = await _mediator.Send(new UpdatePaymentStatusCommand(request));

        return new ScResult<PaymentOperation>(response);
    } 
}