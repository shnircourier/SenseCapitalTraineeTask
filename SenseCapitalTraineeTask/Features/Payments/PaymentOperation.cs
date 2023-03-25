using JetBrains.Annotations;

namespace SenseCapitalTraineeTask.Features.Payments;

/// <summary>
/// Модель операции оплаты
/// </summary>
public class PaymentOperation
{
    /// <summary>
    /// Идентификатор оплаты
    /// </summary>
    public Guid Id { get; [UsedImplicitly] set; }

    /// <summary>
    /// Состояние оплаты
    /// </summary>
    [UsedImplicitly]
    public PaymentState State { get; set; }
    
    /// <summary>
    /// Дата создания оплаты
    /// </summary>
    [UsedImplicitly]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Дата подтверждения оплаты
    /// </summary>
    [UsedImplicitly]
    public DateTime? ConfirmedAt { get; set; }

    /// <summary>
    /// Дата отмены оплаты
    /// </summary>
    [UsedImplicitly]
    public DateTime? CanceledAt { get; set; }

    /// <summary>
    /// Описание оплаты
    /// </summary>
    [UsedImplicitly]
    public string? Description { get; set; }
}