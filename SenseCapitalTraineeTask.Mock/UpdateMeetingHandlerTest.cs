using AutoMapper;
using SC.Internship.Common.Exceptions;
using SenseCapitalTraineeTask.Data;
using SenseCapitalTraineeTask.Data.Entities;
using SenseCapitalTraineeTask.Features.Meetings;
using SenseCapitalTraineeTask.Features.Meetings.UpdateMeeting;

namespace SenseCapitalTraineeTask.Mock;

public class UpdateMeetingHandlerTest
{
    private readonly Mock<IRepository<Meeting>> _meetingRepositoryMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly UpdateMeetingHandler _sut;

    public UpdateMeetingHandlerTest()
    {
        _sut = new UpdateMeetingHandler(_meetingRepositoryMock.Object, _mapperMock.Object);
    }
    
    [Fact]
    public async Task Handle_Should_ReturnException_WhenIdIsWrong()
    {
        // Arrange
        const string id = "6418443b3f0107b2cfe59aec";
        const string errorMessage = "Мероприятие не найдено";
        var beginAt = DateTime.Now;
        var endAt = DateTime.Now.AddDays(3);
        const string description = "Description Description Description Description Description";
        const string title = "Description";
        const decimal ticketPrice = 0;

        var request = new MeetingRequestDto(beginAt, endAt, title, description, id, id, ticketPrice);

        var command = new UpdateMeetingCommand(request, id);

        _meetingRepositoryMock.Setup(x => x.Get(id)).ReturnsAsync((Meeting)null!);

        // Act

        // Assert
        var exception = await Assert.ThrowsAsync<ScException>(() => _sut.Handle(command, default));
        Assert.Equal(errorMessage, exception.Message);
    }
}