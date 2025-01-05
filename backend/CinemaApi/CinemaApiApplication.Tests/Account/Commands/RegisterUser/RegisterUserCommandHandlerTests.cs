using CinemaApiDomain.Entities;
using CinemaApiDomain.Interfaces;
using MediatR;
using Moq;
using Xunit;

namespace CinemaApiApplication.Account.Commands.RegisterUser.Tests
{
    public class RegisterUserCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly RegisterUserCommandHandler _handler;

        public RegisterUserCommandHandlerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _handler = new RegisterUserCommandHandler(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_UserAlreadyExists_ThrowsInvalidOperationException()
        {
            // Arrange
            var existingUser = new User { Email = "test@example.com" };
            _userRepositoryMock
                .Setup(repo => repo.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(existingUser);

            var command = new RegisterUserCommand
            {
                Email = "test@example.com",
                Password = "password123",
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                Nationality = "Polish"
            };

            // Act & Assert
            await Xunit.Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));

            _userRepositoryMock.Verify(repo => repo.GetByEmailAsync(It.IsAny<string>()), Times.Once);
            _userRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<User>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ValidRequest_AddsNewUser()
        {
            // Arrange
            _userRepositoryMock
                .Setup(repo => repo.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((User?)null); // Brak istniejącego użytkownika

            _userRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            var command = new RegisterUserCommand
            {
                Email = "newuser@example.com",
                Password = "password123",
                FirstName = "Jane",
                LastName = "Smith",
                DateOfBirth = new DateTime(1995, 5, 5),
                Nationality = "Polish"
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Xunit.Assert.Equal(Unit.Value, result);
            _userRepositoryMock.Verify(repo => repo.GetByEmailAsync(It.Is<string>(email => email == command.Email)), Times.Once);
            _userRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task Handle_NewUser_HasDefaultRole()
        {
            // Arrange
            _userRepositoryMock
                .Setup(repo => repo.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((User?)null);

            User? addedUser = null;
            _userRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<User>()))
                .Callback<User>(user => addedUser = user)
                .Returns(Task.CompletedTask);

            var command = new RegisterUserCommand
            {
                Email = "newuser@example.com",
                Password = "password123",
                FirstName = "Anna",
                LastName = "Kowalska",
                DateOfBirth = new DateTime(1988, 12, 12),
                Nationality = "Polish"
            };

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            Xunit.Assert.NotNull(addedUser);
            Xunit.Assert.Equal("newuser@example.com", addedUser.Email);
            Xunit.Assert.Single(addedUser.UserRoles);
            Xunit.Assert.Equal(3, addedUser.UserRoles.First().RoleId);
        }

    }
}