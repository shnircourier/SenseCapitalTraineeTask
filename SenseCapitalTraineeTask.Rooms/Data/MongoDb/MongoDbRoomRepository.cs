using MongoDB.Driver;
using SenseCapitalTraineeTask.Rooms.Data.Entities;

namespace SenseCapitalTraineeTask.Rooms.Data.MongoDb;

public class MongoDbRoomRepository : IRepository<Room>
{
    private readonly string _collection;
    private readonly MongoDbConnectionFactory<Room> _connection;
    
    public MongoDbRoomRepository(IConfiguration configuration)
    {
        _collection = configuration["Mongo:RoomCollection"]!;
        _connection = new MongoDbConnectionFactory<Room>(configuration);
    }
    
    public async Task<List<Room>> Get()
    {
        var result = await _connection
            .ConnectToMongo(_collection)
            .FindAsync(_ => true);

        return result.ToList();
    }

    public async Task<Room> Get(string id)
    {
        var result = await _connection
            .ConnectToMongo(_collection)
            .FindAsync(i => i.Id == id);

        return result.FirstOrDefault();
    }

    public async Task<List<Room>> CreateMany(List<Room> entities)
    {
        await _connection
            .ConnectToMongo(_collection)
            .InsertManyAsync(entities);

        return entities;
    }
}