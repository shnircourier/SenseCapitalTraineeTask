using MongoDB.Driver;
using SenseCapitalTraineeTask.Data.Entities;
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

namespace SenseCapitalTraineeTask.Data.MongoDb;

/// <inheritdoc />
public class MongoDbTicketRepository : IRepository<Ticket>
{
    private readonly string _collection;
    private readonly MongoDbConnectionFactory<Ticket> _connection;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration">Конфигурация</param>
    public MongoDbTicketRepository(IConfiguration configuration)
    {
        _collection = configuration["Mongo:TicketCollection"]!;
        _connection = new MongoDbConnectionFactory<Ticket>(configuration);
    }

    /// <inheritdoc />
    public async Task<List<Ticket>> Get()
    {
        var result = await _connection
            .ConnectToMongo(_collection)
            .FindAsync(_ => true);

        return result.ToList();
    }

    /// <inheritdoc />
    public async Task<Ticket> Get(string id)
    {
        var result = await _connection
            .ConnectToMongo(_collection)
            .FindAsync(m => m.Id == id);

        return result.FirstOrDefault();
    }

    /// <inheritdoc />
    public Task<List<Ticket>> GetMeetingsByRoomId(string id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<Ticket> Create(Ticket entity)
    {
        await _connection
            .ConnectToMongo(_collection)
            .InsertOneAsync(entity);

        return entity;
    }

    /// <inheritdoc />
    public async Task<Ticket> Update(Ticket entity)
    {
        var filter = Builders<Ticket>.Filter.Eq("Id", entity.Id);
        
        // ReSharper disable once UnusedVariable
        var result = await _connection
            .ConnectToMongo(_collection)
            .ReplaceOneAsync(filter, entity, new ReplaceOptions { IsUpsert = false });

        return entity;
    }

    /// <inheritdoc />
    public Task UpdateManyImageId(string imageId, string? newValue)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<Ticket> Delete(Ticket entity)
    {
        await _connection
            .ConnectToMongo(_collection)
            .DeleteOneAsync(m => m.Id == entity.Id);
        
        return entity;
    }

    /// <inheritdoc />
    public Task DeleteManyMeetingByRoomId(string roomId)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<List<Ticket>> CreateMany(List<Ticket> entities)
    {
        await _connection
            .ConnectToMongo(_collection)
            .InsertManyAsync(entities);

        return entities;
    }
}