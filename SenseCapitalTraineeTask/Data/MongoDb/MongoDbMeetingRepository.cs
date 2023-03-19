using MongoDB.Driver;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Data.MongoDb;

public class MongoDbMeetingRepository : IRepository<Meeting>
{
    private readonly IMongoCollection<Meeting> _collection;

    public MongoDbMeetingRepository(IConfiguration configuration)
    {
        _collection = MongoDbConnectionFarctory<Meeting>.ConnectToMongo(
            configuration["Mongo:MeetingCollection"]!,
            configuration["Mongo:TestTaskDb"]!,
            configuration["Mongo:ConnectionString"]!);
    }
    
    public async Task<List<Meeting>> Get()
    {
        var result = await _collection.FindAsync(_ => true);

        return result.ToList();
    }

    public async Task<Meeting> Get(string id)
    {
        var result = await _collection.FindAsync(m => m.Id == id);

        return result.FirstOrDefault();
    }

    public async Task<Meeting> Create(Meeting entity)
    {
        await _collection.InsertOneAsync(entity);

        return entity;
    }

    public async Task<Meeting> Update(Meeting entity)
    {
        var filter = Builders<Meeting>.Filter.Eq("Id", entity.Id);

        var result = await _collection.ReplaceOneAsync(filter, entity, new ReplaceOptions { IsUpsert = false });

        return entity;
    }

    public async Task<Meeting> Delete(Meeting entity)
    {
        await _collection.DeleteOneAsync(m => m.Id == entity.Id);
        
        return entity;
    }

    public Task<List<Meeting>> CreateMany(List<Meeting> entities)
    {
        throw new NotImplementedException();
    }
}