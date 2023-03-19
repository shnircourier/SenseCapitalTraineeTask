using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Data.Seeds;

public static class MongoDbRoomSeeder
{
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