using System.Text.Json;
using MediatR;
using Polly;
using Polly.Retry;
using SC.Internship.Common.ScResult;
using SenseCapitalTraineeTask.Identity;

namespace SenseCapitalTraineeTask.Features.Payments.CreatePayment;

public class CreatePaymentHandler : IRequestHandler<CreatePaymentCommand, ScResult<PaymentOperation>>
{
    private const int MaxRetries = 3;
    private readonly IdentityService _identityService;
    private readonly ILogger<CreatePaymentHandler> _logger;
    private readonly AsyncRetryPolicy<ScResult<PaymentOperation>> _retryPolicy;

    public CreatePaymentHandler(IdentityService identityService, ILogger<CreatePaymentHandler> logger)
    {
        _identityService = identityService;
        _logger = logger;
        _retryPolicy = Policy<ScResult<PaymentOperation>>.Handle<HttpRequestException>().RetryAsync(MaxRetries);
    }
    
    public async Task<ScResult<PaymentOperation>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var client = await _identityService.GetAuthorizedClient();
        
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var paymentUrl = Environment.GetEnvironmentVariable("ASPNETCORE_PAYMENT_URL");
            
            _logger.LogInformation("Создание оплаты");

            var json = JsonSerializer.Serialize(request);
            
            var response = await client.PostAsync(paymentUrl + "payments", new StringContent(json), cancellationToken);
            
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