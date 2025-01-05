using CinemaApiDomain.Entities;
using CinemaApiInfrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CinemaApiInfrastructure.Repositories.Tests
{
    public class MovieRepositoryTests
    {
        private readonly CinemaApiDbContext _dbContext;
        private readonly MovieRepository _movieRepository;

        public MovieRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<CinemaApiDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new CinemaApiDbContext(options);
            _movieRepository = new MovieRepository(_dbContext);

            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetAll_ShouldReturnEmptyList_WhenNoMoviesInDatabase()
        {
            // Arrange
            Xunit.Assert.Empty(_dbContext.Movies);

            // Act
            var movies = await _movieRepository.GetAll();

            // Assert
            Xunit.Assert.Empty(movies);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllMovies_WhenMoviesExistInDatabase()
        {
            // Arrange
            var movie1 = new Movie { Title = "Movie 1", DurationInMin = 90, ImageUrl = "Test Image 1", Description = "descr 1" };
            var movie2 = new Movie { Title = "Movie 2", DurationInMin = 120, ImageUrl = "Test Image 2", Description = "descr 2" };

            _dbContext.Movies.AddRange(movie1, movie2);
            await _dbContext.SaveChangesAsync();

            // Act
            var movies = await _movieRepository.GetAll();

            // Assert
            Xunit.Assert.NotEmpty(movies);
            Xunit.Assert.Equal(2, movies.Count());
            Xunit.Assert.Contains(movies, m => m.Title == "Movie 1" && m.DurationInMin == 90);
            Xunit.Assert.Contains(movies, m => m.Title == "Movie 2" && m.DurationInMin == 120);
        }

        [Fact]
        public async Task GetAll_ShouldNotReturnDeletedMovies()
        {
            // Arrange
            var movie1 = new Movie { Title = "Active Movie", DurationInMin = 90, ImageUrl = "Test Image 1", Description = "descr 1" };
            var movie2 = new Movie { Title = "Deleted Movie", DurationInMin = 120, ImageUrl = "Test Image 2", Description = "descr 2" };

            _dbContext.Movies.AddRange(movie1, movie2);
            await _dbContext.SaveChangesAsync();

            // Symulacja usunięcia filmu
            _dbContext.Movies.Remove(movie2);
            await _dbContext.SaveChangesAsync();

            // Act
            var movies = await _movieRepository.GetAll();

            // Assert
            Xunit.Assert.Single(movies); // Powinien zostać tylko jeden film
            Xunit.Assert.Contains(movies, m => m.Title == "Active Movie");
            Xunit.Assert.DoesNotContain(movies, m => m.Title == "Deleted Movie");
        }
    }
}