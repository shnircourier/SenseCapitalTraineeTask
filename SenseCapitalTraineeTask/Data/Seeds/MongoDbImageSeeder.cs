using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Data.Seeds;

public static class MongoDbImageSeeder
{
    public static void Populate(IRepository<Image> repository)
    {
        if (repository.Get().Result.Count == 0)
        {
            var images = repository.CreateMany(new List<Image>
            {
                new(),
                new(),
                new()
            });
        }
    }
}