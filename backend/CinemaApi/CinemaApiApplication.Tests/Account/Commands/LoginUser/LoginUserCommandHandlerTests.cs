using CinemaApiDomain.Entities;
using CinemaApiDomain.Interfaces;
using Microsoft.Extensions.Configuration;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Xunit;

namespace CinemaApiApplication.Account.Commands.LoginUser.Tests
{
    public class LoginUserCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly LoginUserCommandHandler _handler;

        public LoginUserCommandHandlerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _configurationMock = new Mock<IConfiguration>();

            _configurationMock.Setup(config => config["Jwt:Key"]).Returns("supersecretkey12345678901234567890");
            _configurationMock.Setup(config => config["Jwt:Issuer"]).Returns("testIssuer");
            _configurationMock.Setup(config => config["Jwt:Audience"]).Returns("testAudience");

            _handler = new LoginUserCommandHandler(_userRepositoryMock.Object, _configurationMock.Object);
        }

        [Fact]
        public async Task Handle_UserDoesNotExist_ThrowsInvalidOperationException()
        {
            // Arrange
            var command = new LoginUserCommand { Email = "test@example.com", Password = "password" };
            _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync((User?)null);

            // Act & Assert
            var exception = await Xunit.Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
            Xunit.Assert.Equal("Invalid email or password.", exception.Message);
        }

        [Fact]
        public async Task Handle_InvalidPassword_ThrowsInvalidOperationException()
        {
            // Arrange
            var user = new User { Email = "test@example.com", PasswordHash = BCrypt.Net.BCrypt.HashPassword("correctPassword") };
            var command = new LoginUserCommand { Email = "test@example.com", Password = "wrongPassword" };

            _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(It.Is<string>(email => email == command.Email))).ReturnsAsync(user);

            // Act & Assert
            var exception = await Xunit.Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
            Xunit.Assert.Equal("Invalid email or password.", exception.Message);
        }

        [Fact]
        public async Task Handle_ValidCredentials_ReturnsJwtToken()
        {
            // Arrange
            var user = new User
            {
                Email = "test@example.com",
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                Nationality = "USA",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"),
                UserRoles = new List<UserRole> { new UserRole { Role = new Role { Name = "Admin" } } }
            };

            var command = new LoginUserCommand { Email = "test@example.com", Password = "password" };

            _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(It.Is<string>(email => email == command.Email))).ReturnsAsync(user);

            // Act
            var token = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Xunit.Assert.NotNull(token);
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            Xunit.Assert.Equal("testIssuer", jwtToken.Issuer);
            Xunit.Assert.Contains(jwtToken.Claims, c => c.Type == "Email" && c.Value == "test@example.com");
            Xunit.Assert.Contains(jwtToken.Claims, c => c.Type == ClaimTypes.Role && c.Value == "Admin");
        }
    }
}