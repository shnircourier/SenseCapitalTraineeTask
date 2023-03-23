using AutoMapper;
using SenseCapitalTraineeTask.Data;
using SenseCapitalTraineeTask.Data.Entities;
using SenseCapitalTraineeTask.Features.Meetings;
using SenseCapitalTraineeTask.Features.Meetings.MeetingById;

namespace SenseCapitalTraineeTask.Mock;

public class GetMeetingByIdHandlerTest
{
    private readonly Mock<IRepository<Meeting>> _meetingRepositoryMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly GetMeetingByIdHandler _sut;

    public GetMeetingByIdHandlerTest()
    {
        _sut = new GetMeetingByIdHandler(_meetingRepositoryMock.Object, _mapperMock.Object);
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
        

        var query = new GetMeetingByIdQuery(id);
        
        var meeting = new Meeting
        {
            Id = id,
            BeginAt = beginAt,
            EndAt = endAt,
            Description = description,
            Title = title,
            ImgId = id,
            RoomId = id,
            IsFull = isFull,
            Tickets = tickets
        };

        var meetingDto = new MeetingResponseDto(id, beginAt, endAt, title, description, id, id, tickets, isFull);
        
        _meetingRepositoryMock.Setup(x => x.Get(id))
            .ReturnsAsync(meeting);
        _mapperMock.Setup(x => x.Map<MeetingResponseDto>(meeting)).Returns(meetingDto);
        
        // Act

        var result = await _sut.Handle(query, default);
        
        // Assert
        Assert.Equal(meetingDto, result);
    }
}