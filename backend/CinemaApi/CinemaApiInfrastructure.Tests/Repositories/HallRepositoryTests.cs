using CinemaApiDomain.Entities;
using CinemaApiInfrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CinemaApiInfrastructure.Repositories.Tests
{
    public class HallRepositoryTests
    {
        private readonly CinemaApiDbContext _dbContext;
        private readonly HallRepository _hallRepository;

        public HallRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<CinemaApiDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Unikalna baza dla każdego testu
                .Options;

            _dbContext = new CinemaApiDbContext(options);
            _hallRepository = new HallRepository(_dbContext);

            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetHallForGivenSeance_ShouldReturnNull_WhenNoMatchingSeance()
        {
            // Arrange
            var hall = new Hall { Name = "Hall 1", Capacity = 100 };
            var seance = new Seance { Date = DateTime.UtcNow, Movie = new Movie { Title = "Test Movie", DurationInMin = 120, ImageUrl = "Test image url", Description = "desc" } };
            hall.Seances.Add(seance);
            _dbContext.Halls.Add(hall);
            await _dbContext.SaveChangesAsync();

            var nonExistingDate = DateTime.UtcNow.AddDays(1);

            // Act
            var result = await _hallRepository.GetHallForGivenSeance(nonExistingDate);

            // Assert
            Xunit.Assert.Null(result);
        }

        [Fact]
        public async Task GetHallForGivenSeance_ShouldReturnHallWithSeance_WhenSeanceExists()
        {
            // Arrange
            var seanceDate = DateTime.UtcNow.AddHours(3);
            var movie = new Movie 
            { 
                Title = "Test Movie", 
                DurationInMin = 120,
                Description = "Test description",
                ImageUrl = "Test image url"
            };
            var hall = new Hall
            {
                Name = "Hall 1",
                Capacity = 100,
                Seances = new List<Seance>
            {
                new Seance { Date = seanceDate, Movie = movie }
            }
            };

            _dbContext.Halls.Add(hall);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _hallRepository.GetHallForGivenSeance(seanceDate);

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Equal("Hall 1", result.Name);
            Xunit.Assert.Single(result.Seances);
            Xunit.Assert.Equal(seanceDate, result.Seances.First().Date);
        }

        [Fact]
        public async Task GetHallForGivenSeance_ShouldIncludeTicketsAndSeats()
        {
            // Arrange
            var seanceDate = DateTime.UtcNow.AddHours(3);
            var reservationCode = Guid.NewGuid().ToString();

            var movie = new Movie 
            { 
                Title = "Test Movie", 
                DurationInMin = 120,
                Description = "Test description",
                ImageUrl = "Test image url"
            };

            var ticket = new Ticket
            {
                UserEmail = "test@email",
                ReservationCode = reservationCode,
                Status = CinemaApiDomain.Entities.Enums.TicketState.Valid,
                Seats = new List<Seat>
                {
                    new Seat { Row = 1, Number = 1, VIP = false },
                    new Seat { Row = 1, Number = 2, VIP = false }
                }
            };

            var hall = new Hall
            {
                Name = "Hall 1",
                Capacity = 100,
                Seances = new List<Seance>
                {
                    new Seance
                    {
                        Date = seanceDate,
                        Movie = movie,
                        Tickets = new List<Ticket> { ticket }
                    }
                }
            };

            _dbContext.Halls.Add(hall);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _hallRepository.GetHallForGivenSeance(seanceDate);

            // Assert
            Xunit.Assert.NotNull(result);
            Xunit.Assert.Single(result.Seances);
            var seance = result.Seances.First();
            Xunit.Assert.Single(seance.Tickets);
            var resultTicket = seance.Tickets.First();
            Xunit.Assert.Equal(reservationCode, resultTicket.ReservationCode);
            Xunit.Assert.Equal(2, resultTicket.Seats.Count);
            Xunit.Assert.Contains(resultTicket.Seats, s => s.Row == 1 && s.Number == 1 && s.VIP == false);
        }
    }
}