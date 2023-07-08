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
    public class EventsTest
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
            var expectedEvent = new Event { Id = 1, Title = "Event1", Date = DateTime.Now, Location = "Location1", Description = "Description1" };

            var data = new List<Event>
    {
        expectedEvent,
        new Event { Id = 2, Title = "Event2", Date = DateTime.Now, Location = "Location2", Description = "Description2" },
    }.AsQueryable();

            var mockSet = BuildMockDbSet(data);

            mockSet.Setup(d => d.FindAsync(It.IsAny<object[]>())).Returns<object[]>(ids => new ValueTask<Event>(data.FirstOrDefault(d => d.Id == (int)ids[0])));

            var mockContext = new Mock<IEventHorizonDbContext>();
            mockContext.Setup(c => c.Events).Returns(mockSet.Object);

            var controller = new EventsController(mockContext.Object);

            // Act
            var result = await controller.GetEvent(expectedEvent.Id);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Event>>(result);
            var returnValue = Assert.IsType<OkObjectResult>(actionResult.Result);
            var value = Assert.IsType<Event>(returnValue.Value);
            Assert.Equal(expectedEvent.Id, value.Id);
        }

        [Fact]
        public async Task PostEvent_CreatesNewEvent()
        {
            // Arrange
            var mockSet = new Mock<DbSet<Event>>();
            var mockContext = new Mock<IEventHorizonDbContext>();
            mockContext.Setup(m => m.Events).Returns(mockSet.Object);

            var controller = new EventsController(mockContext.Object);
            var newEvent = new Event { Title = "NewEvent", Date = DateTime.Now, Location = "LocationNew", Description = "DescriptionNew" };

            // Act
            var result = await controller.PostEvent(newEvent);

            // Assert
            mockSet.Verify(m => m.Add(It.Is<Event>(e => e.Title == "NewEvent")), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());

            var actionResult = Assert.IsType<ActionResult<Event>>(result);
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
            var returnValue = Assert.IsType<Event>(createdAtActionResult.Value);
            Assert.Equal(newEvent.Title, returnValue.Title);
        }

        [Fact]
        public async Task PutEvent_UpdatesEvent()
        {
            // Arrange
            var eventData = new List<Event>
    {
        new Event { Id = 1, Title = "Event1", Date = DateTime.Now, Location = "Location1", Description = "Description1" }
    }.AsQueryable();

            var mockSet = new Mock<DbSet<Event>>();
            mockSet.As<IQueryable<Event>>().Setup(m => m.Provider).Returns(eventData.Provider);
            mockSet.As<IQueryable<Event>>().Setup(m => m.Expression).Returns(eventData.Expression);
            mockSet.As<IQueryable<Event>>().Setup(m => m.ElementType).Returns(eventData.ElementType);
            mockSet.As<IQueryable<Event>>().Setup(m => m.GetEnumerator()).Returns(eventData.GetEnumerator());

            var mockContext = new Mock<IEventHorizonDbContext>();
            mockContext.Setup(m => m.Events).Returns(mockSet.Object);

            var controller = new EventsController(mockContext.Object);
            var updatedEvent = new Event { Id = 1, Title = "UpdatedEvent", Date = DateTime.Now, Location = "UpdatedLocation", Description = "UpdatedDescription" };

            // Act
            var result = await controller.PutEvent(updatedEvent.Id, updatedEvent);

            // Assert
            mockContext.Verify(m => m.SetModifiedState(It.IsAny<Event>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteEvent_RemovesEvent()
        {
            // Arrange
            var expectedEvent = new Event { Id = 1, Title = "Event1", Date = DateTime.Now, Location = "Location1", Description = "Description1" };
            var data = new List<Event>
    {
        expectedEvent,
        new Event { Id = 2, Title = "Event2", Date = DateTime.Now, Location = "Location2", Description = "Description2" },
    }.AsQueryable();

            var mockSet = BuildMockDbSet(data);

            mockSet.Setup(d => d.FindAsync(It.IsAny<object[]>())).Returns<object[]>(ids => new ValueTask<Event>(data.FirstOrDefault(d => d.Id == (int)ids[0])));

            var mockContext = new Mock<IEventHorizonDbContext>();
            mockContext.Setup(c => c.Events).Returns(mockSet.Object);

            var controller = new EventsController(mockContext.Object);

            // Act
            var result = await controller.DeleteEvent(expectedEvent.Id);

            // Assert
            mockSet.Verify(m => m.Remove(It.IsAny<Event>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
            Assert.IsType<NoContentResult>(result);
        }


    }
}
