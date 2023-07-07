using System;
using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using EventHorizonBackend.Controllers;
using EventHorizonBackend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EventHorizonBackend.Controllers.Events;
using EventHorizonBackend.Data;
using MockQueryable.Moq;

namespace UnitTests
{
    public class GetEventsTest
    {
        [Fact]
        public async Task GetEvents_ReturnsCorrectType()
        {
            // Arrange
            var expectedEvents = new List<Event>()
            {
                new Event { Id = 1, Title = "Event1", Date = DateTime.Now, Location = "Location1", Description = "Description1" },
                new Event { Id = 2, Title = "Event2", Date = DateTime.Now, Location = "Location2", Description = "Description2" }
            }.AsQueryable();

            var mockSet = expectedEvents.BuildMockDbSet();

            var mockContext = new Mock<IEventHorizonDbContext>();
            mockContext.Setup(c => c.Events).Returns(mockSet.Object);

            var controller = new EventsController(mockContext.Object);

            // Act
            var result = await controller.GetEvents();

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Event>>>(result);
            var events = Assert.IsType<List<Event>>(actionResult.Value);
        }


        private Mock<DbSet<T>> BuildMockDbSet<T>(IQueryable<T> data) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mockSet.Setup(d => d.FindAsync(It.IsAny<object[]>())).Returns(new ValueTask<T>(data.First()));

            return mockSet;
        }

        [Fact]
        public async Task GetEvent_ReturnsCorrectEvent()
        {
            // Arrange
            var mockContext = new Mock<IEventHorizonDbContext>();

            var expectedEvent = new Event { Id = 1, Title = "Event1", Date = DateTime.Now, Location = "Location1", Description = "Description1" };
            var events = new List<Event>() { expectedEvent }.AsQueryable();

            var mockSet = BuildMockDbSet(events);
            mockContext.Setup(c => c.Events).Returns(mockSet.Object);

            var controller = new EventsController(mockContext.Object);

            // Act
            var result = await controller.GetEvent(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Event>>(result);
            var returnValue = Assert.IsType<OkObjectResult>(actionResult.Result);
            var eventItem = Assert.IsType<Event>(returnValue.Value);

            Assert.Equal(expectedEvent.Id, eventItem.Id);
            Assert.Equal(expectedEvent.Title, eventItem.Title);
            Assert.Equal(expectedEvent.Location, eventItem.Location);
            Assert.Equal(expectedEvent.Description, eventItem.Description);
            // Assert more properties...
        }

    }
}
