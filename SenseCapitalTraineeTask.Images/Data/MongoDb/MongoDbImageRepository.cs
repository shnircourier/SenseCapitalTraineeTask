using MongoDB.Driver;
using SenseCapitalTraineeTask.Images.Data.Entities;

namespace SenseCapitalTraineeTask.Images.Data.MongoDb;

public class MongoDbImageRepository : IRepository<Image>
{
    private readonly string _collection;
    private readonly MongoDbConnectionFactory<Image> _connection;
    
    public MongoDbImageRepository(IConfiguration configuration)
    {
        _collection = configuration["Mongo:ImageCollection"]!;
        _connection = new MongoDbConnectionFactory<Image>(configuration);
    }
    
    public async Task<List<Image>> Get()
    {
        var result = await _connection
            .ConnectToMongo(_collection)
            .FindAsync(_ => true);

        return result.ToList();
    }
    
    public async Task<Image> Get(string id)
    {
        var result = await _connection
            .ConnectToMongo(_collection)
            .FindAsync(i => i.Id == id);

        return result.FirstOrDefault();
    }
    
    public async Task<List<Image>> CreateMany(List<Image> entities)
    {
        await _connection
            .ConnectToMongo(_collection)
            .InsertManyAsync(entities);

        return entities;
    }
}