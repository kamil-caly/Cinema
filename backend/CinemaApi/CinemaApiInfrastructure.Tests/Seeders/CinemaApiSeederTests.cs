using CinemaApiInfrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CinemaApiInfrastructure.Seeders.Tests
{
    public class CinemaApiSeederTests
    {
        private readonly CinemaApiDbContext _dbContext;
        private readonly CinemaApiSeeder _seeder;

        public CinemaApiSeederTests()
        {
            var options = new DbContextOptionsBuilder<CinemaApiDbContext>()
                .UseInMemoryDatabase(databaseName: "CinemaApiTestDb")
                .Options;

            _dbContext = new CinemaApiDbContext(options);
            _seeder = new CinemaApiSeeder(_dbContext);

            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
        }

        [Fact]
        public async Task Seed_ShouldAddMovies_WhenMoviesAreMissing()
        {
            // Arrange
            Xunit.Assert.Empty(_dbContext.Movies);

            // Act
            await _seeder.Seed();

            // Assert
            Xunit.Assert.NotEmpty(_dbContext.Movies);
            Xunit.Assert.Equal(10, _dbContext.Movies.Count()); // 10 filmów w metodzie GetMovies
        }

        [Fact]
        public async Task Seed_ShouldAddHalls_WhenHallsAreMissing()
        {
            // Arrange
            Xunit.Assert.Empty(_dbContext.Halls);

            // Act
            await _seeder.Seed();

            // Assert
            Xunit.Assert.NotEmpty(_dbContext.Halls);
            Xunit.Assert.Equal(10, _dbContext.Halls.Count()); // 10 sal w metodzie GetHalls
        }

        [Fact]
        public async Task Seed_ShouldAddRoles_WhenRolesAreMissing()
        {
            // Arrange
            Xunit.Assert.Empty(_dbContext.Roles);

            // Act
            await _seeder.Seed();

            // Assert
            Xunit.Assert.NotEmpty(_dbContext.Roles);
            Xunit.Assert.Equal(3, _dbContext.Roles.Count()); // 3 role w metodzie GetRoles
        }

        [Fact]
        public async Task Seed_ShouldAddUsers_WhenUsersAreMissing()
        {
            // Arrange
            Xunit.Assert.Empty(_dbContext.Users);

            // Act
            await _seeder.Seed();

            // Assert
            Xunit.Assert.NotEmpty(_dbContext.Users);
            Xunit.Assert.Equal(2, _dbContext.Users.Count()); // 2 domyślnych użytkowników w metodzie GetDefaultUsers
        }

        [Fact]
        public async Task Seed_ShouldNotAddData_WhenDataAlreadyExists()
        {
            // Arrange
            await _seeder.Seed();
            int movieCount = _dbContext.Movies.Count();
            int hallCount = _dbContext.Halls.Count();
            int roleCount = _dbContext.Roles.Count();
            int userCount = _dbContext.Users.Count();

            // Act
            await _seeder.Seed(); // Ponownie seedujemy

            // Assert
            Xunit.Assert.Equal(movieCount, _dbContext.Movies.Count());
            Xunit.Assert.Equal(hallCount, _dbContext.Halls.Count());
            Xunit.Assert.Equal(roleCount, _dbContext.Roles.Count());
            Xunit.Assert.Equal(userCount, _dbContext.Users.Count());
        }
    }
}