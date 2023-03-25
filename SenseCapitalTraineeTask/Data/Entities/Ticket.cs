using JetBrains.Annotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SenseCapitalTraineeTask.Data.Entities;

/// <summary>
/// Модель билета
/// </summary>
public class Ticket
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
#pragma warning disable CS8618
    public string Id { get; [UsedImplicitly] set; }
#pragma warning restore CS8618

    /// <summary>
    /// Идентификатор владельца билета
    /// </summary>
    public string? OwnerId { get; set; }

    /// <summary>
    /// Номер места
    /// </summary>
    public int? Seat { get; init; }
}