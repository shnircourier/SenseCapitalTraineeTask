namespace SenseCapitalTraineeTask.Features.Meetings;

/// <summary>
/// Модель мероприятия
/// </summary>
/// <param name="BeginAt">Дата начала</param>
/// <param name="EndAt">Дата конца</param>
/// <param name="Title">Заголовок</param>
/// <param name="Description">Описание</param>
/// <param name="ImgId">Id картинки</param>
/// <param name="RoomId">Id комнаты</param>
/// <param name="TicketPrice">Цена за билет</param>
public record MeetingRequestDto(
    DateTime BeginAt,
    DateTime EndAt,
    string Title,
    string Description,
    string ImgId,
    string RoomId,
    decimal TicketPrice
);