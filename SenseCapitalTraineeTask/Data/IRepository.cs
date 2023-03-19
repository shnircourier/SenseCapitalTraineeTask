using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Data;

public interface IRepository<T>
{
    Task<List<T>> Get();

    Task<T> Get(string id);

    Task<T> Create(T entity);

    Task<T> Update(T entity);

    Task<T> Delete(T entity);

    Task<List<T>> CreateMany(List<T> entities);
}