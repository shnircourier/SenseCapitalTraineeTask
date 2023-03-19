using MongoDB.Driver;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Data.MongoDb;

public static class MongoDbConnectionFarctory<T>
{
    public static IMongoCollection<T> ConnectToMongo(string collectionName, string database, string url)
    {
        var client = new MongoClient(url);
        var db = client.GetDatabase(database);
        return db.GetCollection<T>(collectionName);
    }
}