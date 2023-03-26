using MongoDB.Driver;

// ReSharper disable CommentTypo
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

namespace SenseCapitalTraineeTask.Features.Meetings.Data.MongoDb;

/// <summary>
/// Функция связи с mongo
/// </summary>
/// <typeparam name="T"></typeparam>
public class MongoDbConnectionFactory<T>
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration">Конфигурация</param>
    public MongoDbConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    /// <summary>
    /// Метод обращения к mongo
    /// </summary>
    /// <param name="collectionName"></param>
    /// <returns></returns>
    public IMongoCollection<T> ConnectToMongo(string collectionName)
    {
        var client = new MongoClient(Environment.GetEnvironmentVariable("ASPNETCORE_MONGO") ?? _configuration["Mongo:ConnectionString"]);
        var db = client.GetDatabase(_configuration["Mongo:Database"]);
        return db.GetCollection<T>(collectionName);
    }
}