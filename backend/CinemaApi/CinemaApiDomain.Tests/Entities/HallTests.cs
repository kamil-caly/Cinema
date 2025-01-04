using FluentAssertions;
using Xunit;

namespace CinemaApiDomain.Entities.Tests
{
    public class HallTests
    {
        [Fact()]
        public void Capacity_ShouldSetNewValue()
        {
            // arrange
            Hall hall = new Hall();
            Hall hall2 = new Hall();
            Hall hall3 = new Hall();

            // act
            hall.Capacity = 16;
            hall2.Capacity = 361;
            hall3.Capacity = 1024;

            // assert
            hall.Capacity.Should().Be(16);
            hall2.Capacity.Should().Be(361);
            hall3.Capacity.Should().Be(1024);
        }

        [Fact()]
        public void Capacity_ShouldThrowException_WhenValueIsNotAPerfectSquare()
        {
            // arrange
            Hall hall = new Hall();

            // act
            Action action = () => hall.Capacity = 19;

            // assert
            action.Invoking(a => a.Invoke())
                .Should()
                .Throw<Exception>().WithMessage("Value: 19 is not a perfect square.");
        }
    }
}