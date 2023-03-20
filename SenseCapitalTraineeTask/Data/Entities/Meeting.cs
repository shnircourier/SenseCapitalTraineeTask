using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SenseCapitalTraineeTask.Data.Entities;

/// <summary>
/// Модель мероприятия
/// </summary>
public class Meeting
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    // Id инициализируется неявным образом
    public string Id { get; set; }

    public DateTime BeginAt { get; set; }

    public DateTime EndAt { get; set; }

    // Title не может быть null
    public string Title { get; set; }

    // Description не может быть null
    public string Description { get; set; }

    // ImgId не может быть null
    public string ImgId { get; set; }

    // RoomId не может быть null
    public string RoomId { get; set; }

    public List<Ticket> Tickets { get; set; } = new();

    public bool IsFull { get; set; }
}