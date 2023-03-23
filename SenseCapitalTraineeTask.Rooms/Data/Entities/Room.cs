using JetBrains.Annotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SenseCapitalTraineeTask.Rooms.Data.Entities;

/// <summary>
/// Модель заглушки помещения
/// </summary>
public class Room
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
#pragma warning disable CS8618
    public string Id { get; [UsedImplicitly] set; }
#pragma warning restore CS8618
}