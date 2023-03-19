using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Data.Seeds;

/// <summary>
/// Сидер пользователей
/// </summary>
public static class MongoDbUserSeeder
{
    /// <summary>
    /// Метод заполнения бд данными
    /// </summary>
    /// <param name="repository"></param>
    public static void Populate(IRepository<User> repository)
    {
        if (repository.Get().Result.Count == 0)
        {
            var resp = repository.CreateMany(new List<User>
            {
                new()
                {
                    Password = "Password",
                    Username = "Password",
                },
                new()
                {
                    Password = "Password1",
                    Username = "Password1",
                }
            }).Result;
        }
    }
}