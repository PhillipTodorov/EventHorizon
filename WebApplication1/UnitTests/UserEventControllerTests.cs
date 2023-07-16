using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using EventHorizonBackend.Controllers.UserEvents;
using EventHorizonBackend.Data;
using EventHorizonBackend.Models;

public class UserEventsControllerTests
{
    private UserEventsController _controller;
    private Mock<EventHorizonDbContext> _contextMock;

    public UserEventsControllerTests()
    {
        // Initialize mock DbContext
        _contextMock = new Mock<EventHorizonDbContext>();

        // Initialize controller with mock DbContext
        _controller = new UserEventsController(_contextMock.Object);
    }

    [Fact]
    public async Task GetUserEvents_ShouldReturnUserEvents_WhenCalled()
    {
        // Arrange
        var userEvents = Enumerable.Range(1, 5)
            .Select(i => new UserEvent { UserId = i, EventId = i })
            .AsQueryable();

        var dbSetMock = new Mock<DbSet<UserEvent>>();
        dbSetMock.As<IQueryable<UserEvent>>().Setup(m => m.Provider).Returns(userEvents.Provider);
        dbSetMock.As<IQueryable<UserEvent>>().Setup(m => m.Expression).Returns(userEvents.Expression);
        dbSetMock.As<IQueryable<UserEvent>>().Setup(m => m.ElementType).Returns(userEvents.ElementType);
        dbSetMock.As<IQueryable<UserEvent>>().Setup(m => m.GetEnumerator()).Returns(userEvents.GetEnumerator());

        _contextMock.Setup(x => x.UserEvents).Returns(dbSetMock.Object);

        // Act
        var result = await _controller.GetUserEvents();

        // Assert
        Assert.Equal(5, result.Value.Count());
    }
}
