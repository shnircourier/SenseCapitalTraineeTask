namespace SenseCapitalTraineeTask.Rooms.Data;

public interface IRepository<T> 
{
    Task<List<T>> Get();

    Task<T> Get(string id);

    Task<List<T>> CreateMany(List<T> entities);
}