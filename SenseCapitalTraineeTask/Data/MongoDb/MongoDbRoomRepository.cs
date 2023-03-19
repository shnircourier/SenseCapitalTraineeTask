using MongoDB.Driver;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Data.MongoDb;

/// <inheritdoc />
public class MongoDbRoomRepository : IRepository<Room>
{
    private readonly string _collection;
    private readonly MongoDbConnectionFactory<Room> _connection;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration">Конфиг</param>
    public MongoDbRoomRepository(IConfiguration configuration)
    {
        _collection = configuration["Mongo:RoomCollection"]!;
        _connection = new MongoDbConnectionFactory<Room>(configuration);
    }

    /// <inheritdoc />
    public async Task<List<Room>> Get()
    {
        var result = await _connection
            .ConnectToMongo(_collection)
            .FindAsync(_ => true);

        return result.ToList();
    }

    /// <inheritdoc />
    public Task<Room> Get(string id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<Room> Create(Room entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<Room> Update(Room entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<Room> Delete(Room entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<List<Room>> CreateMany(List<Room> entities)
    {
        await _connection
            .ConnectToMongo(_collection)
            .InsertManyAsync(entities);

        return entities;
    }
}