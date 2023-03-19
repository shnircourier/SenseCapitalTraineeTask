using MongoDB.Driver;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Data.MongoDb;

public class MongoDbMeetingRepository : IRepository<Meeting>
{
    private readonly string _collection;
    private readonly MongoDbConnectionFarctory<Meeting> _connection;

    public MongoDbMeetingRepository(IConfiguration configuration)
    {
        _collection = configuration["Mongo:MeetingCollection"]!;
        _connection = new MongoDbConnectionFarctory<Meeting>(configuration);
    }
    
    public async Task<List<Meeting>> Get()
    {
        var result = await _connection
            .ConnectToMongo(_collection)
            .FindAsync(_ => true);

        return result.ToList();
    }

    public async Task<Meeting> Get(string id)
    {
        var result = await _connection
            .ConnectToMongo(_collection)
            .FindAsync(m => m.Id == id);

        return result.FirstOrDefault();
    }

    public async Task<Meeting> Create(Meeting entity)
    {
        await _connection
            .ConnectToMongo(_collection)
            .InsertOneAsync(entity);

        return entity;
    }

    public async Task<Meeting> Update(Meeting entity)
    {
        var filter = Builders<Meeting>.Filter.Eq("Id", entity.Id);

        var result = await _connection
            .ConnectToMongo(_collection)
            .ReplaceOneAsync(filter, entity, new ReplaceOptions { IsUpsert = false });

        return entity;
    }

    public async Task<Meeting> Delete(Meeting entity)
    {
        await _connection
            .ConnectToMongo(_collection)
            .DeleteOneAsync(m => m.Id == entity.Id);
        
        return entity;
    }

    public Task<List<Meeting>> CreateMany(List<Meeting> entities)
    {
        throw new NotImplementedException();
    }
}