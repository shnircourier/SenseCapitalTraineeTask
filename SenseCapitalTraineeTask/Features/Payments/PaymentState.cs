using JetBrains.Annotations;

namespace SenseCapitalTraineeTask.Features.Payments;

/// <summary>
/// Тип статуса оплаты
/// </summary>
public enum PaymentState
{
    /// <summary>
    /// На рассмотрении
    /// </summary>
    [UsedImplicitly] Hold = 0,
    /// <summary>
    /// Подтвержден
    /// </summary>
    Confirmed = 1,
    /// <summary>
    /// Отменен
    /// </summary>
    Canceled = 2
}