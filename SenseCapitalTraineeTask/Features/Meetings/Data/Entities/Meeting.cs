using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SenseCapitalTraineeTask.Features.Meetings.Data.Entities;

/// <summary>
/// Модель мероприятия
/// </summary>
public class Meeting
{
#pragma warning disable CS8618
    /// <summary>
    /// Идентификатор
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    /// <summary>
    /// Дата начала
    /// </summary>
    public DateTime BeginAt { get; set; }

    /// <summary>
    /// Дата конца
    /// </summary>
    public DateTime EndAt { get; set; }
    
    /// <summary>
    /// Заголовок
    /// </summary>
    public string Title { get; set; }

    // Description не может быть null
    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Id картинки
    /// </summary>
    public string ImgId { get; set; }
    
    /// <summary>
    /// Id помещения
    /// </summary>
    public string RoomId { get; set; }

    /// <summary>
    /// Лист билетов
    /// </summary>
    public List<Ticket> Tickets { get; set; } = new();

    /// <summary>
    /// Заполнено ли помещение
    /// </summary>
    public bool IsFull { get; set; }

    /// <summary>
    /// Цена за билет
    /// </summary>
    public decimal TicketPrice { get; set; }
    
#pragma warning restore CS8618
}