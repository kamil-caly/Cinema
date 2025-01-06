using CinemaApiApplication.Seance;
using CinemaApiApplication.Seat;
using CinemaApiApplication.Ticket;
using CinemaApiApplication.Ticket.Commands.CreateTicketForGivenSeance;
using CinemaApiApplication.Ticket.Queries.GetTicketsForGivenUser;
using CinemaApiDomain.Entities;
using CinemaApiDomain.Entities.Enums;
using CinemaApiDomain.Interfaces;
using CinemaWebApi.Tests;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace CinemaWebApi.Controllers.Tests
{
    public class TicketControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<ISeanceRepository> _seanceRepositoryMock = new();
        private readonly Mock<ISeatRepository> _seatRepositoryMock = new();
        private readonly Mock<ITicketRepository> _ticketRepositoryMock = new();
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock = new();
        public TicketControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>();

                    services.Replace(ServiceDescriptor.Scoped(typeof(ISeanceRepository), _ => _seanceRepositoryMock.Object));
                    services.Replace(ServiceDescriptor.Scoped(typeof(ISeatRepository), _ => _seatRepositoryMock.Object));
                    services.Replace(ServiceDescriptor.Scoped(typeof(ITicketRepository), _ => _ticketRepositoryMock.Object));
                    services.Replace(ServiceDescriptor.Singleton(typeof(IHttpContextAccessor), _ => _httpContextAccessorMock.Object));
                });
            });
        }


        [Fact]
        public async Task CreateTicket_ShouldReturn200Ok_WhenTicketIsCreated()
        {
            // Arrange
            var seanceDate = DateTime.Now.AddDays(5);
            var seance = new Seance { Id = 1, Date = seanceDate };
            var email = "test@example.com";

            _httpContextAccessorMock.Setup(h => h.HttpContext)
                .Returns(new DefaultHttpContext
                {
                    User = new System.Security.Claims.ClaimsPrincipal(new System.Security.Claims.ClaimsIdentity(
                        [
                            new System.Security.Claims.Claim("Email", email)
                        ]))
                });

            _seanceRepositoryMock.Setup(s => s.GetSeanceForGivenDate(seanceDate)).ReturnsAsync(seance);
            _seatRepositoryMock.Setup(s => s.GetSeatsForGivenSeanceDate(seanceDate)).ReturnsAsync(new List<Seat>());

            var command = new CreateTicketForGivenSeanceCommand
            {
                SeanceDate = seanceDate,
                SeatDtos = new List<SeatDto>
                {
                    new SeatDto { Row = 1, Num = 1, VIP = false }
                }
            };

            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync("/api/ticket/createTicket", command);
            var reservationCode = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            reservationCode.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task GetTickets_ShouldReturnError500_WhenSeatsAreTaken()
        {
            // Arrange
            var seanceDate = DateTime.Now.AddDays(5);
            var seance = new Seance { Id = 1, Date = seanceDate };
            var email = "test@example.com";

            _httpContextAccessorMock.Setup(h => h.HttpContext)
                .Returns(new DefaultHttpContext
                {
                    User = new System.Security.Claims.ClaimsPrincipal(new System.Security.Claims.ClaimsIdentity(
                        [
                            new System.Security.Claims.Claim("Email", email)
                        ]))
                });

            _seanceRepositoryMock.Setup(s => s.GetSeanceForGivenDate(seanceDate)).ReturnsAsync(seance);
            _seatRepositoryMock.Setup(s => s.GetSeatsForGivenSeanceDate(seanceDate)).ReturnsAsync(new List<Seat>
            {
                new Seat { Row = 1, Number = 2, VIP = false }
            });

            var command = new CreateTicketForGivenSeanceCommand
            {
                SeanceDate = seanceDate,
                SeatDtos = new List<SeatDto>
                {
                    new SeatDto { Row = 1, Num = 1, VIP = false },
                    new SeatDto { Row = 1, Num = 2, VIP = false }
                }
            };

            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync("/api/ticket/createTicket", command);
            string? errorMsg = await response.Content.ReadFromJsonAsync<string>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
            errorMsg.Should().NotBeNullOrWhiteSpace();
            errorMsg.Should().Be("Chosen seats are taken");
        }

        [Fact]
        public async Task GetTickets_ShouldReturnError500_WhenEmailFromClaimsIsNull()
        {
            // Arrange
            var email = "test@example.com";

            _httpContextAccessorMock.Setup(h => h.HttpContext)
                .Returns(new DefaultHttpContext
                {
                    User = new System.Security.Claims.ClaimsPrincipal(new System.Security.Claims.ClaimsIdentity(
                        [
                            new System.Security.Claims.Claim("Test", email)
                        ]))
                });

            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/ticket/getTickets?userRequest=true");
            string? errorMsg = await response.Content.ReadFromJsonAsync<string>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
            errorMsg.Should().NotBeNullOrWhiteSpace();
            errorMsg.Should().Be("User email is null");
        }

        [Fact]
        public async Task GetTickets_ShouldReturn200Ok_WhenUserHasEmailInClaims()
        {
            // Arrange
            var email = "test@example.com";

            _httpContextAccessorMock.Setup(h => h.HttpContext)
                .Returns(new DefaultHttpContext
                {
                    User = new System.Security.Claims.ClaimsPrincipal(new System.Security.Claims.ClaimsIdentity(
                        [
                            new System.Security.Claims.Claim("Email", email)
                        ]))
                });

            var tickets = new List<Ticket>() 
            { 
                new Ticket() 
                {
                    Id = 1,
                    ReservationCode = "Test",
                    Status = TicketState.Valid,
                    PurchaseDate = DateTime.Now,
                    UserEmail = email,
                    Seance = new Seance
                    {
                        Id = 1,
                        Date = DateTime.Now.AddDays(5),
                        Movie = new Movie { Title = "Test Movie", Description = "Test Decr", DurationInMin = 100, ImageUrl = "Test Image url" },
                        Hall = new Hall { Name = "Test Hall", Capacity = 144 }
                    },
                    Seats = new List<Seat>
                    {
                        new Seat { Row = 1, Number = 1, VIP = false }
                    },  
                }
            };

            _ticketRepositoryMock.Setup(t => t.GetTicketsAsync(email)).ReturnsAsync(tickets);

            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/ticket/getTickets?userRequest=true");
            var ticketsDto = await response.Content.ReadFromJsonAsync<IEnumerable<UserTicketDto>>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            ticketsDto.Should().NotBeNull();
            ticketsDto.Should().HaveCount(1);
            var ticketDto = ticketsDto!.First();
            ticketDto.ReservationCode.Should().Be("Test");
            ticketDto.SeatDtos.Should().HaveCount(1);
            var seatDto = ticketDto.SeatDtos.First();
            seatDto.Row.Should().Be(1);
            seatDto.Num.Should().Be(1);
            seatDto.VIP.Should().BeFalse();
        }

        [Fact]
        public async Task ChangeTicketState_ShouldReturnError500_WhenTicketNotFound()
        {
            // Arrange
            var reservationCode = "123456789";
            _ticketRepositoryMock.Setup(t => t.GetTicketAsync(reservationCode)).ReturnsAsync((Ticket?)null);

            var client = _factory.CreateClient();

            // Act
            var response = await client.PutAsync($"/api/ticket/changeState?reservationCode={reservationCode}", null);
            string? errorMsg = await response.Content.ReadFromJsonAsync<string>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
            errorMsg.Should().NotBeNullOrWhiteSpace();
            errorMsg.Should().Be("Ticket for given reservation code not found");
        }

        [Fact]
        public async Task ChangeTicketState_ShouldReturn200Ok_WhenTicketWithGivenReservationCodeExists()
        {
            // Arrange
            var reservationCode = "123456789";
            var ticket = new Ticket()
            {
                Id = 1,
                ReservationCode = reservationCode,
                Status = TicketState.Valid,
                PurchaseDate = DateTime.Now,
                UserEmail = "test@email",
                Seance = new Seance
                {
                    Id = 1,
                    Date = DateTime.Now.AddDays(5)
                },
                Seats = new List<Seat>
                {
                    new Seat { Row = 1, Number = 1, VIP = false }
                }
            };

            _ticketRepositoryMock.Setup(t => t.GetTicketAsync(reservationCode)).ReturnsAsync(ticket);

            var client = _factory.CreateClient();

            // Act
            var response = await client.PutAsync($"/api/ticket/changeState?reservationCode={reservationCode}", null);
            bool isUpdated = await response.Content.ReadFromJsonAsync<bool>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            isUpdated.Should().Be(true);
        }
    }
}