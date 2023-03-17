namespace SenseCapitalTraineeTask.Data.Entities;

public class Ticket
{
    public Guid Id { get; set; }

    public Guid? OwnerId { get; set; }

    public int Seat { get; set; }
}