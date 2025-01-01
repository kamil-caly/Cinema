using CinemaApiApplication.Seat;
using MediatR;

namespace CinemaApiApplication.Hall.Queries
{
    public class GetHallForGivenSeanceDateQuery : IRequest<HallDto?>
    {
        public DateTime _dateTime { get; set; }
        public GetHallForGivenSeanceDateQuery(DateTime dateTime)
        {
            _dateTime = dateTime;
        }
    }
}
