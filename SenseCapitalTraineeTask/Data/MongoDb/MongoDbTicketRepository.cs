using MongoDB.Driver;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Data.MongoDb;

/// <inheritdoc />
public class MongoDbTicketRepository : IRepository<Ticket>
{
    private readonly string _collection;
    private readonly MongoDbConnectionFactory<Ticket> _connection;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration">Конфиг</param>
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

        var result = await _connection
            .ConnectToMongo(_collection)
            .ReplaceOneAsync(filter, entity, new ReplaceOptions { IsUpsert = false });

        return entity;
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
    public async Task<List<Ticket>> CreateMany(List<Ticket> entities)
    {
        await _connection
            .ConnectToMongo(_collection)
            .InsertManyAsync(entities);

        return entities;
    }
}