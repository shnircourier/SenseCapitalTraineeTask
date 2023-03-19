using MongoDB.Driver;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Data.MongoDb;

public class MongoDbRoomRepository : IRepository<Room>
{
    private readonly string _collection;
    private readonly MongoDbConnectionFarctory<Room> _connection;

    public MongoDbRoomRepository(IConfiguration configuration)
    {
        _collection = configuration["Mongo:ImageCollection"]!;
        _connection = new MongoDbConnectionFarctory<Room>(configuration);
    }
    public async Task<List<Room>> Get()
    {
        var result = await _connection
            .ConnectToMongo(_collection)
            .FindAsync(_ => true);

        return result.ToList();
    }

    public Task<Room> Get(string id)
    {
        throw new NotImplementedException();
    }

    public Task<Room> Create(Room entity)
    {
        throw new NotImplementedException();
    }

    public Task<Room> Update(Room entity)
    {
        throw new NotImplementedException();
    }

    public Task<Room> Delete(Room entity)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Room>> CreateMany(List<Room> entities)
    {
        await _connection
            .ConnectToMongo(_collection)
            .InsertManyAsync(entities);

        return entities;
    }
}