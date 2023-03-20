using MongoDB.Driver;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Data.MongoDb;

/// <inheritdoc />
public class MongoDbMeetingRepository : IRepository<Meeting>
{
    private readonly string _collection;
    private readonly MongoDbConnectionFactory<Meeting> _connection;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration">Конфиг</param>
    public MongoDbMeetingRepository(IConfiguration configuration)
    {
        _collection = configuration["Mongo:MeetingCollection"]!;
        _connection = new MongoDbConnectionFactory<Meeting>(configuration);
    }

    /// <inheritdoc />
    public async Task<List<Meeting>> Get()
    {
        var result = await _connection
            .ConnectToMongo(_collection)
            .FindAsync(_ => true);

        return result.ToList();
    }

    /// <inheritdoc />
    public async Task<Meeting> Get(string id)
    {
        var result = await _connection
            .ConnectToMongo(_collection)
            .FindAsync(m => m.Id == id);

        return result.FirstOrDefault();
    }

    /// <inheritdoc />
    public async Task<Meeting> Create(Meeting entity)
    {
        await _connection
            .ConnectToMongo(_collection)
            .InsertOneAsync(entity);

        return entity;
    }

    /// <inheritdoc />
    public async Task<Meeting> Update(Meeting entity)
    {
        var filter = Builders<Meeting>.Filter.Eq("Id", entity.Id);

        //Переменная не используется но необходима поскольку метод CreateMany имеет результат
        var result = await _connection
            .ConnectToMongo(_collection)
            .ReplaceOneAsync(filter, entity, new ReplaceOptions { IsUpsert = false });

        return entity;
    }

    /// <inheritdoc />
    public async Task<Meeting> Delete(Meeting entity)
    {
        await _connection
            .ConnectToMongo(_collection)
            .DeleteOneAsync(m => m.Id == entity.Id);
        
        return entity;
    }

    /// <inheritdoc />
    public Task<List<Meeting>> CreateMany(List<Meeting> entities)
    {
        throw new NotImplementedException();
    }
}