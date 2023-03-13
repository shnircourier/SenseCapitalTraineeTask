namespace Data;

public interface IRepository<T>
{
    List<T> Get();

    T Get(Guid id);

    T Create(T entity);

    T Update(T entity);

    T Delete(Guid id);
}