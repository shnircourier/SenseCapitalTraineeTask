using AutoMapper;
using SenseCapitalTraineeTask.Data;
using SenseCapitalTraineeTask.Data.Entities;
using SenseCapitalTraineeTask.Features.Meetings;
using SenseCapitalTraineeTask.Features.Meetings.CreateMeeting;

namespace SenseCapitalTraineeTask.Mock;

public class CreateMeetingHandlerTest
{
    private readonly Mock<IRepository<Meeting>> _meetingRepositoryMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly CreateMeetingHandler _sut;

    public CreateMeetingHandlerTest()
    {
        _sut = new CreateMeetingHandler(_meetingRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task Handle_Should_ReturnMeeting_WhenMeetingExists()
    {
        // Arrange
        const string id = "6418443b3f0107b2cfe59aec";
        var beginAt = DateTime.Now;
        var endAt = DateTime.Now.AddDays(3);
        const string description = "Description Description Description Description Description";
        const string title = "Description";
        const bool isFull = false;
        var tickets = new List<Ticket>();
        const decimal ticketPrice = 0;

        var request = new MeetingRequestDto(beginAt, endAt, title, description, id, id, ticketPrice);
        
        var meeting = new Meeting
        {
            BeginAt = beginAt,
            EndAt = endAt,
            Description = description,
            Title = title,
            ImgId = id,
            RoomId = id,
            IsFull = isFull,
            Tickets = tickets
        };
        
        var response = new MeetingResponseDto(id, beginAt, endAt, title, description, id, id, tickets, isFull, ticketPrice);
        
        var command = new CreateMeetingCommand(request);
        
        _mapperMock.Setup(x => x.Map<Meeting>(request)).Returns(meeting);
        
        _meetingRepositoryMock.Setup(x => x.Create(meeting)).ReturnsAsync(meeting);

        _mapperMock.Setup(x => x.Map<MeetingResponseDto>(meeting)).Returns(response);
        
        // Act

        var result = await _sut.Handle(command, default);
        
        // Assert
        Assert.Equal(response, result);
    }
}