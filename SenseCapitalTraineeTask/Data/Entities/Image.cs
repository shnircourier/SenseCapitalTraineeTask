using JetBrains.Annotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SenseCapitalTraineeTask.Data.Entities;

/// <summary>
/// Модель заглушка картинок
/// </summary>
public class Image
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    // Id инициализируется неявным образом
    public string Id { get; [UsedImplicitly] set; }
}