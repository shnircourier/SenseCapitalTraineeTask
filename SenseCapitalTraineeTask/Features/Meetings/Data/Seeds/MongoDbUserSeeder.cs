using SenseCapitalTraineeTask.Features.Meetings.Data.Entities;

// ReSharper disable IdentifierTypo

namespace SenseCapitalTraineeTask.Features.Meetings.Data.Seeds;

/// <summary>
/// Тестовые данные пользователей
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
            var _ = repository.CreateMany(new List<User>
            {
                new()
                {
                    Password = "Password",
                    Username = "Password"
                },
                new()
                {
                    Password = "Password1",
                    Username = "Password1"
                }
            }).Result;
        }
    }
}