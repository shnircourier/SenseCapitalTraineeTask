using MongoDB.Driver;

namespace SenseCapitalTraineeTask.Data.MongoDb;

public class MongoDbConnectionFarctory<T>
{
    private readonly IConfiguration _configuration;

    public MongoDbConnectionFarctory(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public IMongoCollection<T> ConnectToMongo(string collectionName)
    {
        var client = new MongoClient(_configuration["Mongo:ConnectionString"]);
        var db = client.GetDatabase(_configuration["Mongo:Database"]);
        return db.GetCollection<T>(collectionName);
    }
}