using Application.User.UserCommands.CreateUser;
using Application.User.UserCommands.DeleteUser;
using Application.User.UserCommands.UpdateUser;
using Application.User.UserQueries.GetAll;
using Application.User.UserQueries.GetById;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class UserTest
    {
        private Mock<UserManager<IdentityUser>> _userManagerMock;
        private Mock<ILogger<CreateUserCommandHandler>> _createLoggerMock;
        private Mock<ILogger<DeleteUserByIdCommandHandler>> _deleteLoggerMock;
        private Mock<ILogger<UpdateUserByIdCommandHandler>> _updateLoggerMock;
        private Mock<ILogger<GetUserByIdQueryHandler>> _getByIdLoggerMock;
        private Mock<ILogger<GetAllUserQueryHandler>> _getAllLoggerMock;

        [SetUp]
        public void Setup()
        {
            // Mocka IUserStore och UserManager
            var userStoreMock = new Mock<IUserStore<IdentityUser>>();
            _userManagerMock = new Mock<UserManager<IdentityUser>>
              (userStoreMock.Object, null, null, null, null, null, null, null, null);

            // Mocka ILogger för varje kommando
            _createLoggerMock = new Mock<ILogger<CreateUserCommandHandler>>();
            _deleteLoggerMock = new Mock<ILogger<DeleteUserByIdCommandHandler>>();
            _updateLoggerMock = new Mock<ILogger<UpdateUserByIdCommandHandler>>();
            _getByIdLoggerMock = new Mock<ILogger<GetUserByIdQueryHandler>>();
            _getAllLoggerMock = new Mock<ILogger<GetAllUserQueryHandler>>();
        }

        [Test]
        public async Task CreateUserCommandHandler_ShouldCreateUserSuccessfully()
        {
            // Arrange
            var handler = new CreateUserCommandHandler(_userManagerMock.Object, _createLoggerMock.Object);
            var command = new CreateUserCommand
            {
                Username = "testuser",
                Email = "test@example.com",
                Password = "Password123!"
            };

            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<IdentityUser>(), command.Password))
                            .ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert       
            Assert.That(result.Succeeded, Is.True);

            _userManagerMock.Verify(um => um.CreateAsync(It.Is<IdentityUser>(
             user => user.UserName == command.Username && user.Email == command.Email),
             command.Password), Times.Once);
        }

        [Test]
        public async Task CreateUser_ShouldReturnFailure_WhenCreationFails()
        {
            var handler = new CreateUserCommandHandler(_userManagerMock.Object, _createLoggerMock.Object);
            var command = new CreateUserCommand { Username = "testuser", Email = "test@example.com", Password = "Test@123" };

            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<IdentityUser>(), command.Password))
                            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Creation failed" }));
            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.Succeeded, Is.False);
            Assert.That(result.Errors, Is.Not.Empty);
           
        }

        [Test]
        public async Task UpdateUserByIdCommandHandler_ShouldUpdateUserSuccessfully()
        {
            // Arrange
            var handler = new UpdateUserByIdCommandHandler(_userManagerMock.Object, _updateLoggerMock.Object);
            var command = new UpdateUserByIdCommand
            {
                UserId = "user-id",
                Username = "new-username",
                Email = "new-email@example.com"
            };

            var user = new IdentityUser { Id = command.UserId, UserName = "old-username", Email = "old-email@example.com" };

            _userManagerMock.Setup(um => um.FindByIdAsync(command.UserId)).ReturnsAsync(user);
            _userManagerMock.Setup(um => um.UpdateAsync(It.IsAny<IdentityUser>())).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.Succeeded, Is.True, "Uppdateringen borde ha lyckats.");
            Assert.That(user.UserName, Is.EqualTo("new-username"));
            Assert.That(user.Email, Is.EqualTo("new-email@example.com"));

            _userManagerMock.Verify(um => um.UpdateAsync(user), Times.Once);
        }

        [Test]
        public async Task UpdateUser_ShouldFail_WhenUpdateFails()
        {
            // Arrange
            var handler = new UpdateUserByIdCommandHandler(_userManagerMock.Object, _updateLoggerMock.Object);
            var command = new UpdateUserByIdCommand { UserId = "123", Username = "newuser", Email = "new@example.com" };
            var user = new IdentityUser { Id = "123", UserName = "olduser", Email = "old@example.com" };

            _userManagerMock.Setup(um => um.FindByIdAsync(command.UserId)).ReturnsAsync(user);
            _userManagerMock.Setup(um => um.UpdateAsync(It.IsAny<IdentityUser>()))
                   .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Update failed" }));

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result.Succeeded, Is.False);
        }

        [Test]
        public async Task DeleteUserByIdCommandHandler_ShouldDeleteUserSuccessfully()
        {
           // Arrange
           var handler = new DeleteUserByIdCommandHandler(_userManagerMock.Object, _deleteLoggerMock.Object);
           var command = new DeleteUserByIdCommand { UserId = "user-id" };
           var user = new IdentityUser { Id = command.UserId };

           _userManagerMock.Setup(um => um.FindByIdAsync(command.UserId)).ReturnsAsync(user);
           _userManagerMock.Setup(um => um.DeleteAsync(user)).ReturnsAsync(IdentityResult.Success);

           // Act
           var result = await handler.Handle(command, CancellationToken.None);

          // Assert
          Assert.That(result.Succeeded, Is.True);
          _userManagerMock.Verify(um => um.DeleteAsync(user), Times.Once);
        }


        [Test]
        public async Task GetAllUserQueryHandler_ShouldReturnAllUsers()
        {
            var handler = new GetAllUserQueryHandler(_userManagerMock.Object, _getAllLoggerMock.Object);

            var users = new List<IdentityUser>
            {
                new IdentityUser { Id = "user1", UserName = "user1" },
                new IdentityUser { Id = "user2", UserName = "user2" }
            }.AsQueryable();

            _userManagerMock.Setup(um => um.Users).Returns(users);

            var result = await handler.Handle(new GetAllUserQuery(), CancellationToken.None);

            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Select(u => u.UserName).ToList(), Is.EquivalentTo(new List<string> { "user1", "user2" }));
        }


        [Test]
        public async Task GetUserById_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var handler = new GetUserByIdQueryHandler(_userManagerMock.Object, _getByIdLoggerMock.Object);
            var userId = "user-id";
            var user = new IdentityUser { Id = userId, UserName = "testuser" };

            _userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync(user);

            // Act
            var result = await handler.Handle(new GetUserByIdQuery { UserId = userId }, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.UserName, Is.EqualTo("testuser"));
        }

        [Test]
        public async Task GetUserById_ShouldReturnNull_WhenUserNotFound()
        {
            // Arrange
            var handler = new GetUserByIdQueryHandler(_userManagerMock.Object, _getByIdLoggerMock.Object);
            var userId = "non-existent-id";

            _userManagerMock.Setup(um => um.FindByIdAsync(userId)).ReturnsAsync((IdentityUser)null);

            // Act
            var result = await handler.Handle(new GetUserByIdQuery { UserId = userId }, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Null);
        }
    }
}
    


    

