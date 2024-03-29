using System.Text;
using System.Text.Json;
using JetBrains.Annotations;
using MediatR;
using Polly;
using Polly.Retry;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;
using SenseCapitalTraineeTask.Identity;

namespace SenseCapitalTraineeTask.Features.Payments.CreatePayment;

/// <summary>
/// Логика отправки запроса на создание платежа
/// </summary>
[UsedImplicitly]
public class CreatePaymentHandler : IRequestHandler<CreatePaymentRequest, ScResult<PaymentOperation>>
{
    private const int MaxRetries = 3;
    private readonly IdentityService _identityService;
    private readonly ILogger<CreatePaymentHandler> _logger;
    private readonly AsyncRetryPolicy<ScResult<PaymentOperation>> _retryPolicy;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="identityService"></param>
    /// <param name="logger"></param>
    public CreatePaymentHandler(IdentityService identityService, ILogger<CreatePaymentHandler> logger)
    {
        _identityService = identityService;
        _logger = logger;
        _retryPolicy = Policy<ScResult<PaymentOperation>>.Handle<HttpRequestException>().RetryAsync(MaxRetries);
    }

    /// <inheritdoc />
    public async Task<ScResult<PaymentOperation>> Handle(CreatePaymentRequest request, CancellationToken cancellationToken)
    {
        var client = await _identityService.GetAuthorizedClient();
        
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var paymentUrl = Environment.GetEnvironmentVariable("ASPNETCORE_PAYMENT_URL");
            
            _logger.LogInformation("Создание оплаты");

            var json = JsonSerializer.Serialize(request);
            
            var response = await client.PostAsync(paymentUrl + "/payments", new StringContent(json, Encoding.UTF8, "application/json"), cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new ScException("Ошибка при оплате");
            }

            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            
            _logger.LogInformation("Ответ: {0}", content);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var data = JsonSerializer.Deserialize<ScResult<PaymentOperation>>(content, options);

            return data!;
        });
    }
}