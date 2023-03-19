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
    public string Id { get; set; }

    public DateTime BeginAt { get; set; }

    public DateTime EndAt { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string ImgId { get; set; }

    public string RoomId { get; set; }

    public List<Ticket> Tickets { get; set; } = new();

    public bool IsFull { get; set; }
}