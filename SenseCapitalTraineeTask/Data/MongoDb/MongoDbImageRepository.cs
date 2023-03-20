using MongoDB.Driver;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Data.MongoDb;

/// <inheritdoc />
public class MongoDbImageRepository : IRepository<Image>
{
    private readonly string _collection;
    private readonly MongoDbConnectionFactory<Image> _connection;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration">Конфиг</param>
    public MongoDbImageRepository(IConfiguration configuration)
    {
        _collection = configuration["Mongo:ImageCollection"]!;
        _connection = new MongoDbConnectionFactory<Image>(configuration);
    }

    /// <inheritdoc />
    public async Task<List<Image>> Get()
    {
        var result = await _connection
            .ConnectToMongo(_collection)
            .FindAsync(_ => true);

        return result.ToList();
    }

    /// <inheritdoc />
    public Task<Image> Get(string id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<Image> Create(Image entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<Image> Update(Image entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<Image> Delete(Image entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<List<Image>> CreateMany(List<Image> entities)
    {
        await _connection
            .ConnectToMongo(_collection)
            .InsertManyAsync(entities);

        return entities;
    }
}