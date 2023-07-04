//using System;
//using System.Threading.Tasks;
//using Xunit;
//using Moq;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using EventHorizonBackend.Controllers;
//using EventHorizonBackend.Models;

//namespace UnitTests
//{
//    public class UnitTest1
//    {
//        [Fact]
//        public async Task Register_WithValidModel_CreatesUser()
//        {
//            // Arrange
//            var userManagerMock = new Mock<UserManager<IdentityUser>>();
//            var signInManagerMock = new Mock<SignInManager<IdentityUser>>();
//            var usersController = new UsersController(userManagerMock.Object, signInManagerMock.Object);
//            var model = new RegisterViewModel { /* populate with valid data */ };

//            // Act
//            var result = await usersController.Register(model);

//            // Assert
//            userManagerMock.Verify(u => u.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()), Times.Once());
//        }

//    }
//}