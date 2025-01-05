using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace CinemaApiApplication.Account.Queries.GetUserData.Tests
{
    public class GetUserDataQueryHandlerTests
    {
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly GetUserDataQueryHandler _handler;

        public GetUserDataQueryHandlerTests()
        {
            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.Setup(config => config["Jwt:Key"]).Returns("supersecretkey12345678901234567890"); // 32 znaki
            _configurationMock.Setup(config => config["Jwt:Issuer"]).Returns("testIssuer");
            _configurationMock.Setup(config => config["Jwt:Audience"]).Returns("testAudience");

            _handler = new GetUserDataQueryHandler(_configurationMock.Object);
        }

        [Fact]
        public async Task Handle_EmptyToken_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var query = new GetUserDataQuery(token: "");

            // Act & Assert
            var exception = await Xunit.Assert.ThrowsAsync<UnauthorizedAccessException>(() => _handler.Handle(query, CancellationToken.None));
            Xunit.Assert.Equal("Invalid token.", exception.Message);
        }

        [Fact]
        public async Task Handle_MissingClaimsInToken_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var token = GenerateJwtToken(new Claim("Email", "test@example.com")); // Missing other required claims
            var query = new GetUserDataQuery(token);

            // Act & Assert
            var exception = await Xunit.Assert.ThrowsAsync<UnauthorizedAccessException>(() => _handler.Handle(query, CancellationToken.None));
            Xunit.Assert.Equal("Invalid token: Email or FirstName or LastName or Role not exists in token.", exception.Message);
        }

        [Fact]
        public async Task Handle_ValidToken_ReturnsUserDataDto()
        {
            // Arrange
            var claims = new[]
            {
            new Claim("Email", "test@example.com"),
            new Claim("FirstName", "John"),
            new Claim("LastName", "Doe"),
            new Claim(ClaimTypes.Role, "Admin")
        };

            var token = GenerateJwtToken(claims);
            var query = new GetUserDataQuery(token);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal("test@example.com", result.Email);
            Xunit.Assert.Equal("John", result.FirstName);
            Xunit.Assert.Equal("Doe", result.LastName);
            Xunit.Assert.Equal("Admin", result.Role);
        }

        private string GenerateJwtToken(params Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configurationMock.Object["Jwt:Key"] ?? ""));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configurationMock.Object["Jwt:Issuer"],
                audience: _configurationMock.Object["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}