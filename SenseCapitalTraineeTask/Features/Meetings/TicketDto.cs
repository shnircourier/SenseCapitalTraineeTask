namespace SenseCapitalTraineeTask.Features.Meetings;

public class TicketDto
{
    public Guid Id { get; set; }

    public Guid OwnerId { get; set; }

    public int Seat { get; set; }
}