using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Data.Seeds;

/// <summary>
/// Сидер картинок
/// </summary>
public static class MongoDbImageSeeder
{
    /// <summary>
    /// Метод наполнения бд данными
    /// </summary>
    /// <param name="repository"></param>
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