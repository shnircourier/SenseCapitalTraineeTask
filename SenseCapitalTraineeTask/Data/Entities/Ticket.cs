using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SenseCapitalTraineeTask.Data.Entities;

/// <summary>
/// Модель билета
/// </summary>
public class Ticket
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    // Id инициализируется неявным образом
    public string Id { get; set; }

    public string? OwnerId { get; set; }

    public int? Seat { get; set; }
}