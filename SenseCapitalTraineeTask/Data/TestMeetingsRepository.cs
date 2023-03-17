using SenseCapitalTraineeTask.Data.Entities;

namespace SenseCapitalTraineeTask.Data;

public class TestDataRepository : IRepository<Meeting>
{
    private readonly List<Meeting> _meetings = new();
    private readonly HashSet<Guid> _imgGuids = new();
    private readonly HashSet<Guid> _roomGuids = new();

    public TestDataRepository()
    {
        _imgGuids.Add(Guid.NewGuid());
        _imgGuids.Add(Guid.NewGuid());
        _imgGuids.Add(Guid.NewGuid());

        _roomGuids.Add(Guid.NewGuid());
        _roomGuids.Add(Guid.NewGuid());
        _roomGuids.Add(Guid.NewGuid());
    }

    public List<Meeting> Get()
    {
        return _meetings;
    }

    public Meeting Get(Guid id)
    {
        return _meetings.FirstOrDefault(e => e.Id == id);
    }

    public Meeting Create(Meeting meeting)
    {
        meeting.Id = Guid.NewGuid();
        
        _meetings.Add(meeting);

        return meeting;
    }

    public Meeting Update(Meeting meeting)
    {
        var index = _meetings.IndexOf(Get(meeting.Id));

        _meetings[index] = meeting; 

        return meeting;
    }

    public Meeting Delete(Guid id)
    {
        var meeting = Get(id);

        _meetings.Remove(meeting);

        return meeting;
    }

    public HashSet<Guid> GetAvailableImgGuids()
    {
        return _imgGuids;
    }

    public HashSet<Guid> GetAvailableRoomGuids()
    {
        return _roomGuids;
    }

    public Meeting CreateFreeTickets(Meeting meeting)
    {
        return Update(meeting);
    }
}