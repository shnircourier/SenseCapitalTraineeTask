namespace SenseCapitalTraineeTask.Data;

/// <summary>
/// Интерфейс репозитория
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
    /// Удаление записи
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<T> Delete(T entity);

    /// <summary>
    /// Множественное добавление записей
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    Task<List<T>> CreateMany(List<T> entities);
}