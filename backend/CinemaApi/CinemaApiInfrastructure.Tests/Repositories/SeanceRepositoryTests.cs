using CinemaApiDomain.Entities;
using CinemaApiInfrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CinemaApiInfrastructure.Repositories.Tests
{

    public class SeanceRepositoryTests
    {
        private readonly CinemaApiDbContext _dbContext;
        private readonly SeanceRepository _seanceRepository;

        public SeanceRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<CinemaApiDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new CinemaApiDbContext(options);
            _seanceRepository = new SeanceRepository(_dbContext);

            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetAllWithDetailsForGivenArgs_ShouldReturnEmptyList_WhenNoSeancesMatch()
        {
            // Arrange
            var dateTime = DateTime.Now.AddDays(1);
            string? movieTitle = "Nonexistent Movie";

            // Act
            var seances = await _seanceRepository.GetAllWithDetailsForGivenArgs(dateTime, movieTitle);

            // Assert
            Xunit.Assert.Empty(seances);
        }

        [Fact]
        public async Task GetAllWithDetailsForGivenArgs_ShouldReturnMatchingSeances_WhenSeancesExist()
        {
            // Arrange
            var movie = new Movie { Title = "Test Movie", DurationInMin = 90, ImageUrl = "Test Image 1", Description = "descr 1" };
            var hall = new Hall { Name = "Test Hall", Capacity = 100 };
            var seance = new Seance { Date = DateTime.Now.AddDays(1), Movie = movie, Hall = hall };

            _dbContext.Seances.Add(seance);
            await _dbContext.SaveChangesAsync();

            // Act
            var seances = await _seanceRepository.GetAllWithDetailsForGivenArgs(seance.Date, "Test Movie");

            // Assert
            Xunit.Assert.NotEmpty(seances);
            Xunit.Assert.Single(seances);
            Xunit.Assert.Equal("Test Movie", seances.First().Movie.Title);
            Xunit.Assert.Equal("Test Hall", seances.First().Hall.Name);
        }

        [Fact]
        public async Task GetAllWithDetailsForGivenArgs_ShouldReturnNoMatchingSeances_WhenSeancesExistButWithDateInPast()
        {
            // Arrange
            var movie = new Movie { Title = "Test Movie", DurationInMin = 90, ImageUrl = "Test Image 1", Description = "descr 1" };
            var hall = new Hall { Name = "Test Hall", Capacity = 100 };
            var senaceDate = DateTime.Now.AddDays(-2);
            var seance = new Seance { Date = senaceDate, Movie = movie, Hall = hall };
            await _dbContext.Seances.AddAsync(seance);
            await _dbContext.SaveChangesAsync();

            // Act
            var seances = await _seanceRepository.GetAllWithDetailsForGivenArgs(senaceDate, null);

            // Assert
            Xunit.Assert.Empty(seances);
        }

        [Fact]
        public async Task GetSeanceForGivenDate_ShouldReturnNull_WhenNoSeanceExistsForGivenDate()
        {
            // Arrange
            var dateTime = DateTime.Now;

            // Act
            var seance = await _seanceRepository.GetSeanceForGivenDate(dateTime);

            // Assert
            Xunit.Assert.Null(seance);
        }

        [Fact]
        public async Task GetSeanceForGivenDate_ShouldReturnSeance_WhenSeanceExistsForGivenDate()
        {
            // Arrange
            var movie = new Movie { Title = "Test Movie", DurationInMin = 90, ImageUrl = "Test Image 1", Description = "descr 1" };
            var hall = new Hall { Name = "Test Hall", Capacity = 100 };
            var seanceDate = DateTime.Now.AddDays(1);
            var seance = new Seance { Date = seanceDate, Movie = movie, Hall = hall };

            _dbContext.Seances.Add(seance);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _seanceRepository.GetSeanceForGivenDate(seanceDate);

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal(seanceDate, result.Date);
            Xunit.Assert.Equal("Test Movie", result.Movie.Title);
            Xunit.Assert.Equal("Test Hall", result.Hall.Name);
        }
    }
}