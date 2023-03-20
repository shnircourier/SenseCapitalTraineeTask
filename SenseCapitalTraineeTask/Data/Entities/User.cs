using JetBrains.Annotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SenseCapitalTraineeTask.Data.Entities;

/// <summary>
/// Модель пользователя
/// </summary>
public record User
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [UsedImplicitly]
#pragma warning disable CS8618
    public string Id { get; set; }

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Username { [UsedImplicitly] get; set; }
    
    /// <summary>
    /// Пароль пользователя
    /// </summary>
    public string Password { [UsedImplicitly] get; set; }
    
#pragma warning restore CS8618
}