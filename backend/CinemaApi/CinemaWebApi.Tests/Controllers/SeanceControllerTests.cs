using CinemaApiApplication.Seance;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using Xunit;
using FluentAssertions;
using Moq;
using CinemaApiDomain.Interfaces;
using CinemaApiDomain.Entities;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Security.Policy;

namespace CinemaWebApi.Controllers.Tests
{
    public class SeanceControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly Mock<ISeanceRepository> _seanceRepositoryMock = new();

        public SeanceControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.Replace(ServiceDescriptor.Scoped(typeof(ISeanceRepository),
                                                _ => _seanceRepositoryMock.Object));
                });
            });
        }

        [Fact]
        public async Task GetAllForGivenDate_ShouldReturn200OkWithEmptyData_WhenNoSeancesExist()
        {
            // Arrange
            DateTime dateTime = DateTime.Now.AddDays(5).Date;
            _seanceRepositoryMock.Setup(m => m.GetAllWithDetailsForGivenArgs(dateTime, It.IsAny<string>()))
                .ReturnsAsync(new List<Seance>());

            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/api/seance/getAllForGivenDate?dateTime={dateTime.ToString("yyyy-MM-dd")}");
            var seanceDtos = await response.Content.ReadFromJsonAsync<List<SeanceDto>>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            seanceDtos.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAllForGivenDate_ShouldReturn200Ok_WhenSeancesExist()
        {
            // Arrange
            DateTime dateTime = DateTime.Now.AddDays(5).Date;
            string movieTitle = "Test Movie";
            Movie movie = new Movie { Title = movieTitle, DurationInMin = 90, ImageUrl = "Test Image 1", Description = "descr 1" };
            Hall hall = new Hall { Name = "Test Hall", Capacity = 100 };

            _seanceRepositoryMock.Setup(m => m.GetAllWithDetailsForGivenArgs(dateTime, movieTitle))
                .ReturnsAsync(new List<Seance>() { new Seance
                    {
                        Date = dateTime,
                        Movie = movie,
                        Hall = hall
                    }
                });

            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"/api/seance/getAllForGivenDate?dateTime={dateTime.ToString("yyyy-MM-dd")}&movieTitle={movieTitle}");
            var seanceDtos = await response.Content.ReadFromJsonAsync<List<SeanceDto>>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            seanceDtos.Should().NotBeNull();
            seanceDtos.Should().HaveCount(1);
            var seanceDto = seanceDtos!.First();
            seanceDto.SeanceDate.Should().Be(dateTime);
            seanceDto!.MovieTitle.Should().Be(movieTitle);
            seanceDto.HallName.Should().Be(hall.Name);
        }
    }
}