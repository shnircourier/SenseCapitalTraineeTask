using MongoDB.Driver;
using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Data.MongoDb;

public class MongoDbUserRepository : IRepository<User>
{
    private readonly string _collection;
    private readonly MongoDbConnectionFarctory<User> _connection;

    public MongoDbUserRepository(IConfiguration configuration)
    {
        _collection = configuration["Mongo:UserCollection"]!;
        _connection = new MongoDbConnectionFarctory<User>(configuration);
    }
    
    public async Task<List<User>> Get()
    {
        var result = await _connection
            .ConnectToMongo(_collection)
            .FindAsync(_ => true);

        return result.ToList();
    }

    public Task<User> Get(string id)
    {
        throw new NotImplementedException();
    }

    public Task<User> Create(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<User> Update(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<User> Delete(User entity)
    {
        throw new NotImplementedException();
    }

    public async Task<List<User>> CreateMany(List<User> entities)
    {
        await _connection
            .ConnectToMongo(_collection)
            .InsertManyAsync(entities);

        return entities;
    }
}