namespace SenseCapitalTraineeTask.Features.Meetings.Data;

/// <summary>
/// Repository интерфейс
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T>
{
    /// <summary>
    /// Метод получения коллекции данных
    /// </summary>
    /// <returns></returns>
    Task<List<T>> Get();

    /// <summary>
    /// Получение записи по ее ключу
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<T> Get(string id);

    /// <summary>
    /// Получить список мероприятии по определенному помещению
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<List<T>> GetMeetingsByRoomId(string id);

    /// <summary>
    /// Добавление записи
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<T> Create(T entity);

    /// <summary>
    /// Обновление записи
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<T> Update(T entity);

    /// <summary>
    /// Множественное обновление поля ImgId
    /// </summary>
    /// <param name="imageId"></param>
    /// <param name="newValue"></param>
    /// <returns></returns>
    Task UpdateManyImageId(string imageId, string? newValue);

    /// <summary>
    /// Удаление записи
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<T> Delete(T entity);

    /// <summary>
    /// Множественное удаление по id помещения
    /// </summary>
    /// <param name="roomId"></param>
    /// <returns></returns>
    Task DeleteManyMeetingByRoomId(string roomId);

    /// <summary>
    /// Множественное добавление записей
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task<List<T>> CreateMany(List<T> entities);
}