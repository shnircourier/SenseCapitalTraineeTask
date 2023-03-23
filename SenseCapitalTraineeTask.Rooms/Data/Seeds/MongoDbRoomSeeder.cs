using SenseCapitalTraineeTask.Rooms.Data.Entities;

namespace SenseCapitalTraineeTask.Rooms.Data.Seeds;

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
            //Переменная не используется но необходима поскольку метод CreateMany имеет результат
            var rooms = repository.CreateMany(new List<Room>
            {
                new(),
                new(),
                new()
            });
        }
    }
}