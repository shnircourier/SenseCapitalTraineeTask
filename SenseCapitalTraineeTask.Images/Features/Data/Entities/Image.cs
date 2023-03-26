using JetBrains.Annotations;

namespace SenseCapitalTraineeTask.Images.Features.Data.Entities;

/// <summary>
/// Модель заглушка картинок
/// </summary>
public class Image
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public string? Id { get; [UsedImplicitly] set; }
}