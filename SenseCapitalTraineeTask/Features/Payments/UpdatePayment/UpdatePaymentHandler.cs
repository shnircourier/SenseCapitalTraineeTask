using System.Text;
using System.Text.Json;
using JetBrains.Annotations;
using MediatR;
using Polly;
using Polly.Retry;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;
using SenseCapitalTraineeTask.Features.Payments.CreatePayment;
using SenseCapitalTraineeTask.Identity;

namespace SenseCapitalTraineeTask.Features.Payments.UpdatePayment;

/// <summary>
/// Логика отправки запроса на обновления статуса оплаты
/// </summary>
[UsedImplicitly]
public class UpdatePaymentHandler : IRequestHandler<UpdatePaymentRequest, ScResult<PaymentOperation>>
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
    public UpdatePaymentHandler(IdentityService identityService, ILogger<CreatePaymentHandler> logger)
    {
        _identityService = identityService;
        _logger = logger;
        _retryPolicy = Policy<ScResult<PaymentOperation>>.Handle<HttpRequestException>().RetryAsync(MaxRetries);
    }

    /// <inheritdoc />
    public async Task<ScResult<PaymentOperation>> Handle(UpdatePaymentRequest request, CancellationToken cancellationToken)
    {
        var client = await _identityService.GetAuthorizedClient();
        
        return await _retryPolicy.ExecuteAsync(async () =>
        {
            var paymentUrl = Environment.GetEnvironmentVariable("ASPNETCORE_PAYMENT_URL");
            
            _logger.LogInformation("Изменение статуса оплаты: {0}", request.RequestDto.State);

            var json = JsonSerializer.Serialize(request.RequestDto);
            
            var response = await client.PatchAsync(paymentUrl + "/payments/status", new StringContent(json, Encoding.UTF8, "application/json"), cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new ScException("Ошибка при подтверждении оплаты");
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