using MediatR;

namespace CinemaApiApplication.Seance.Queries
{
    public class GetAllSeancesForGivenDateQuery : IRequest<IEnumerable<SeanceDto>?>
    {
        public DateTime _dateTime { get; set; }
        public GetAllSeancesForGivenDateQuery(DateTime dateTime)
        {
            _dateTime = dateTime;
        }
    }
}
