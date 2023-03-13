using Data.Entities;

namespace Data;

public class TestDataRepository : IRepository<Meeting>
{
    private readonly List<Meeting> _meetings = new();

    public TestDataRepository()
    {
        _meetings.Add(new Meeting
        {
            Id = Guid.NewGuid(),
            Title = "О бабочках",
            Description = "Заманчивое описания чтобы приманить как можно больше людей",
            BeginAt = DateTime.Now,
            EndAt = DateTime.Now.AddHours(2),
            ImgId = Guid.NewGuid(),
            RoomId = Guid.NewGuid()
        });
        
        _meetings.Add(new Meeting
        {
            Id = Guid.NewGuid(),
            Title = "Вечеринка",
            Description = "Описание для привлечения людей к этому событию",
            BeginAt = DateTime.Now.AddDays(1),
            EndAt = DateTime.Now.AddDays(1).AddDays(2),
            ImgId = Guid.NewGuid(),
            RoomId = Guid.NewGuid()
        });
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

        _meetings.Insert(index, meeting);

        return meeting;
    }

    public Meeting Delete(Guid id)
    {
        var meeting = Get(id);

        _meetings.Remove(meeting);

        return meeting;
    }
}