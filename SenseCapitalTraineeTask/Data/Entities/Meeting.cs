namespace SenseCapitalTraineeTask.Data.Entities;

public class Meeting
{
    public Guid Id { get; set; }

    public DateTime BeginAt { get; set; }

    public DateTime EndAt { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public Guid ImgId { get; set; }

    public Guid RoomId { get; set; }
}