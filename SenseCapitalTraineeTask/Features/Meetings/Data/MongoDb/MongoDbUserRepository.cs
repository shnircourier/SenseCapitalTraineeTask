using MongoDB.Driver;
using SenseCapitalTraineeTask.Features.Meetings.Data.Entities;

// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

namespace SenseCapitalTraineeTask.Features.Meetings.Data.MongoDb;

/// <inheritdoc />
public class MongoDbUserRepository : IRepository<User>
{
    private readonly string _collection;
    private readonly MongoDbConnectionFactory<User> _connection;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration">Конфигурация</param>
    public MongoDbUserRepository(IConfiguration configuration)
    {
        _collection = configuration["Mongo:UserCollection"]!;
        _connection = new MongoDbConnectionFactory<User>(configuration);
    }

    /// <inheritdoc />
    public async Task<List<User>> Get()
    {
        var result = await _connection
            .ConnectToMongo(_collection)
            .FindAsync(_ => true);

        return result.ToList();
    }

    /// <inheritdoc />
    public Task<User> Get(string id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<List<User>> GetMeetingsByRoomId(string id)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<User> Create(User entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<User> Update(User entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task UpdateManyImageId(string imageId, string? newValue)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task<User> Delete(User entity)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task DeleteManyMeetingByRoomId(string roomId)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<List<User>> CreateMany(List<User> entities)
    {
        await _connection
            .ConnectToMongo(_collection)
            .InsertManyAsync(entities);

        return entities;
    }
}