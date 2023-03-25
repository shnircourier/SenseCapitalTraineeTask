using System.Text.Json;
using MediatR;
using Polly;
using Polly.Retry;
using SC.Internship.Common.ScResult;
using SenseCapitalTraineeTask.Features.Payments.CreatePayment;
using SenseCapitalTraineeTask.Identity;

namespace SenseCapitalTraineeTask.Features.Payments.UpdatePayment;

public class UpdatePaymentHandler : IRequestHandler<UpdatePaymentCommand, ScResult<PaymentOperation>>
{
    private const int MaxRetries = 3;
    private readonly IdentityService _identityService;
    private readonly ILogger<CreatePaymentHandler> _logger;
    private readonly AsyncRetryPolicy<ScResult<PaymentOperation>> _retryPolicy;
    
    public UpdatePaymentHandler(IdentityService identityService, ILogger<CreatePaymentHandler> logger)
    {
        _identityService = identityService;
        _logger = logger;
        _retryPolicy = Policy<ScResult<PaymentOperation>>.Handle<HttpRequestException>().RetryAsync(MaxRetries);
    }
    
    public async Task<ScResult<PaymentOperation>> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
    {
        var client = await _identityService.GetAuthorizedClient();
        
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var paymentUrl = Environment.GetEnvironmentVariable("ASPNETCORE_PAYMENT_URL");
            
            _logger.LogInformation("Изменение статуса оплаты: {0}", request.Request.State);

            var json = JsonSerializer.Serialize(request.Request);
            
            var response = await client.PatchAsync(paymentUrl + "payments/status", new StringContent(json), cancellationToken);
            
            _logger.LogInformation("Ответ: {0}", response);

            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var data = JsonSerializer.Deserialize<ScResult<PaymentOperation>>(content, options);

            return data!;
        });
    }
}