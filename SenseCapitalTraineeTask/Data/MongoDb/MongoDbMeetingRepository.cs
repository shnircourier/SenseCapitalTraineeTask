using MongoDB.Driver;
using SenseCapitalTraineeTask.Data.Entities;
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

namespace SenseCapitalTraineeTask.Data.MongoDb;

/// <inheritdoc />
public class MongoDbMeetingRepository : IRepository<Meeting>
{
    private readonly string _collection;
    private readonly MongoDbConnectionFactory<Meeting> _connection;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration">Конфигурация</param>
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
    public async Task<List<Meeting>> GetMeetingsByRoomId(string id)
    {
        var result = await _connection
            .ConnectToMongo(_collection)
            .FindAsync(m => m.Id == id);

        return result.ToList();
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

        // ReSharper disable once UnusedVariable
        var result = await _connection
            .ConnectToMongo(_collection)
            .ReplaceOneAsync(filter, entity, new ReplaceOptions { IsUpsert = false });

        return entity;
    }

    /// <summary>
    /// Множественное обновление по id картинки
    /// </summary>
    /// <param name="imageId"></param>
    /// <param name="newValue"></param>
    public async Task UpdateManyImageId(string imageId, string? newValue)
    {
        var filter = Builders<Meeting>.Filter.Eq(x => x.ImgId, imageId);
        var update = Builders<Meeting>.Update.Set(x => x.ImgId, newValue);

        // ReSharper disable once UnusedVariable
        var result = await _connection
            .ConnectToMongo(_collection)
            .UpdateManyAsync(filter, update);
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
    public async Task DeleteManyMeetingByRoomId(string roomId)
    {
        var filter = Builders<Meeting>.Filter.Eq(x => x.RoomId, roomId);

        // ReSharper disable once UnusedVariable
        var result = await _connection
            .ConnectToMongo(_collection)
            .DeleteManyAsync(filter);
    }

    /// <inheritdoc />
    public Task<List<Meeting>> CreateMany(List<Meeting> entities)
    {
        throw new NotImplementedException();
    }
}