using JetBrains.Annotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SenseCapitalTraineeTask.Data.Entities;

/// <summary>
/// Модель пользователя
/// </summary>
public record User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [UsedImplicitly]
    // Id инициализируется неявным образом
    public string Id { get; set; }

    // Username не может быть null
    public string Username { [UsedImplicitly] get; set; }

    // Password не может быть null
    public string Password { [UsedImplicitly] get; set; }
}