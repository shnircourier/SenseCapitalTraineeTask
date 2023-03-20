using JetBrains.Annotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SenseCapitalTraineeTask.Data.Entities;

/// <summary>
/// Модель заглушки помещения
/// </summary>
public class Room
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    // Id инициализируется неявным образом
    public string Id { get; [UsedImplicitly] set; }
}