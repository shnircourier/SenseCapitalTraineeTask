using SenseCapitalTraineeTask.Rooms.Data.Entities;

namespace SenseCapitalTraineeTask.Rooms.Data;

public class TestDataRepository : IRepository<Room>
{
    private readonly List<Room> _rooms;

    public TestDataRepository()
    {
        _rooms = new List<Room>
        {
            new()
            {
                Id = "641d8164fe5581c7b7f7f307"
            },
            new()
            {
                Id = "641d8164fe5581c7b7f7f306"
            },
            new()
            {
                Id = "641d8164fe5581c7b7f7f305"
            }
        };
    }
    
    public Task<List<Room>> Get()
    {
        return Task.FromResult(_rooms);
    }

    public Task<Room> Get(string id)
    {
        return Task.FromResult(_rooms.FirstOrDefault(r => r.Id == id))!;
    }
}