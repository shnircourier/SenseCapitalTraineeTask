namespace SenseCapitalTraineeTask.Images.Data;

public interface IRepository<T>
{
    Task<List<T>> Get();

    Task<T> Get(string id);
}