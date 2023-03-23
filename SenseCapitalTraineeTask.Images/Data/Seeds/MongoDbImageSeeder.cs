using SenseCapitalTraineeTask.Images.Data.Entities;

namespace SenseCapitalTraineeTask.Images.Data.Seeds;

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
            //Переменная не используется но необходима поскольку метод CreateMany имеет результат
            var images = repository.CreateMany(new List<Image>
            {
                new(),
                new(),
                new()
            });
        }
    }
}