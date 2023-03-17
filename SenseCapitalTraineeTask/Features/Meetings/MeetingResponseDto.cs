using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Features.Meetings;

/// <summary>
/// 
/// </summary>
/// <param name="BeginAt">Дата начала</param>
/// <param name="EndAt">Дата конца</param>
/// <param name="Title">Заголовок</param>
/// <param name="Description">Описание</param>
/// <param name="ImgId">Id картинки</param>
/// <param name="RoomId">Id комнаты</param>
/// <param name="Tickets">Список билетов</param>
/// <param name="IsFull">Заполнена ли</param>
public record MeetingResponseDto(
    Guid Id,
    DateTime BeginAt,
    DateTime EndAt,
    string Title,
    string Description,
    Guid ImgId,
    Guid RoomId,
    List<Ticket> Tickets,
    bool IsFull);