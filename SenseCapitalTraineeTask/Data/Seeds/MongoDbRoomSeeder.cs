using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Data.Seeds;

/// <summary>
/// Сидер помещений
/// </summary>
public static class MongoDbRoomSeeder
{
    /// <summary>
    /// Метод наполнения бд данными
    /// </summary>
    /// <param name="repository"></param>
    public static void Populate(IRepository<Room> repository)
    {
        if (repository.Get().Result.Count == 0)
        {
            var rooms = repository.CreateMany(new List<Room>
            {
                new(),
                new(),
                new()
            });
        }
    }
}