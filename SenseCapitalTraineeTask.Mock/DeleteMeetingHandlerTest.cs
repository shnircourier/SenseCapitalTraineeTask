using AutoMapper;
using SC.Internship.Common.Exceptions;
using SenseCapitalTraineeTask.Data;
using SenseCapitalTraineeTask.Data.Entities;
using SenseCapitalTraineeTask.Features.Meetings.DeleteMeeting;

namespace SenseCapitalTraineeTask.Mock;

public class DeleteMeetingHandlerTest
{
    private readonly Mock<IRepository<Meeting>> _meetingRepositoryMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly DeleteMeetingHandler _sut;

    public DeleteMeetingHandlerTest()
    {
        _sut = new DeleteMeetingHandler(_meetingRepositoryMock.Object, _mapperMock.Object);
    }
    
    [Fact]
    public async Task Handle_Should_ReturnException_WhenIdIsWrong()
    {
        // Arrange
        const string requestId = "6418443b3f0107b2cfe59aaa";

        const string errorMessage = "Мероприятие не найдено";
        

        var command = new DeleteMeetingCommand(requestId);

        _meetingRepositoryMock.Setup(x => x.Get(requestId))!
            .ReturnsAsync((Meeting)null!);

        // Act

        // Assert
        var exception = await Assert.ThrowsAsync<ScException>(() => _sut.Handle(command, default));
        Assert.Equal(errorMessage, exception.Message);
    }
}