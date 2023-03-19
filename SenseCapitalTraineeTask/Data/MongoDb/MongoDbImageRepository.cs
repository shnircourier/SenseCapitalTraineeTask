using MongoDB.Driver;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Data.MongoDb;

public class MongoDbImageRepository : IRepository<Image>
{
    private readonly string _collection;
    private readonly MongoDbConnectionFarctory<Image> _connection;

    public MongoDbImageRepository(IConfiguration configuration)
    {
        _collection = configuration["Mongo:ImageCollection"]!;
        _connection = new MongoDbConnectionFarctory<Image>(configuration);
    }
    
    public async Task<List<Image>> Get()
    {
        var result = await _connection
            .ConnectToMongo(_collection)
            .FindAsync(_ => true);

        return result.ToList();
    }

    public Task<Image> Get(string id)
    {
        throw new NotImplementedException();
    }

    public Task<Image> Create(Image entity)
    {
        throw new NotImplementedException();
    }

    public Task<Image> Update(Image entity)
    {
        throw new NotImplementedException();
    }

    public Task<Image> Delete(Image entity)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Image>> CreateMany(List<Image> entities)
    {
        await _connection
            .ConnectToMongo(_collection)
            .InsertManyAsync(entities);

        return entities;
    }
}