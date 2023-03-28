using JetBrains.Annotations;

namespace SenseCapitalTraineeTask.Features.Payments;

/// <summary>
/// Запрос на обработку платежа
/// </summary>
public class UpdatePaymentRequestDto
{
    /// <summary>
    /// Идентификатор платежа
    /// </summary>
    public Guid Id { [UsedImplicitly] get; set; }
    
    /// <summary>
    /// Состояние платежа
    /// </summary>
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public PaymentState State { get; set; }
    
    /// <summary>
    /// Описание платежа
    /// </summary>
    public string? Description { [UsedImplicitly] get; set; }
}