using MongoDB.Driver;

namespace SenseCapitalTraineeTask.Images.Data.MongoDb;

/// <summary>
/// Хелпер связи с монго
/// </summary>
/// <typeparam name="T"></typeparam>
public class MongoDbConnectionFactory<T>
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration">Конфиг</param>
    public MongoDbConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    /// <summary>
    /// Метод обращения к монго
    /// </summary>
    /// <param name="collectionName"></param>
    /// <returns></returns>
    public IMongoCollection<T> ConnectToMongo(string collectionName)
    {
        var client = new MongoClient(_configuration["Mongo:ConnectionString"]);
        var db = client.GetDatabase(_configuration["Mongo:Database"]);
        return db.GetCollection<T>(collectionName);
    }
}