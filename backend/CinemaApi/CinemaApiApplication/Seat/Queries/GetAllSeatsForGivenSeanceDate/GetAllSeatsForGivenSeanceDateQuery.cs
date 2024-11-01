using MediatR;

namespace CinemaApiApplication.Seat.Queries.GetAllSeatsForGivenSeanceDate
{
    public class GetAllSeatsForGivenSeanceDateQuery : IRequest<IEnumerable<SeatDto>?>
    {
        public DateTime _dateTime { get; set; }
        public GetAllSeatsForGivenSeanceDateQuery(DateTime dateTime)
        {
            _dateTime = dateTime;
        }
    }
}
