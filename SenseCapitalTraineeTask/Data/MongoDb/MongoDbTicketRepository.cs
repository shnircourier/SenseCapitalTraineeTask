using MongoDB.Driver;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Data.MongoDb;

public class MongoDbTicketRepository : IRepository<Ticket>
{
    private readonly IMongoCollection<Ticket> _collection;

    public MongoDbTicketRepository(IConfiguration configuration)
    {
        _collection = MongoDbConnectionFarctory<Ticket>.ConnectToMongo(
            configuration["Mongo:TicketCollection"]!,
            configuration["Mongo:TestTaskDb"]!,
            configuration["Mongo:ConnectionString"]!);
    }
    
    public async Task<List<Ticket>> Get()
    {
        var result = await _collection.FindAsync(_ => true);

        return result.ToList();
    }

    public async Task<Ticket> Get(string id)
    {
        var result = await _collection.FindAsync(m => m.Id == id);

        return result.FirstOrDefault();
    }

    public async Task<Ticket> Create(Ticket entity)
    {
        await _collection.InsertOneAsync(entity);

        return entity;
    }

    public async Task<Ticket> Update(Ticket entity)
    {
        var filter = Builders<Ticket>.Filter.Eq("Id", entity.Id);

        var result = await _collection.ReplaceOneAsync(filter, entity, new ReplaceOptions { IsUpsert = false });

        return entity;
    }

    public async Task<Ticket> Delete(Ticket entity)
    {
        await _collection.DeleteOneAsync(m => m.Id == entity.Id);
        
        return entity;
    }

    public async Task<List<Ticket>> CreateMany(List<Ticket> entities)
    {
        await _collection.InsertManyAsync(entities);

        return entities;
    }
}