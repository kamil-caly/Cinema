using MediatR;

namespace CinemaApiApplication.Seance.Queries
{
    public class GetAllSeancesWithDetailsForGivenArgsQuery : IRequest<IEnumerable<SeanceDto>>
    {
        public DateTime _dateTime { get; set; }

        public string? _movieTitle { get; set; }

        public GetAllSeancesWithDetailsForGivenArgsQuery(DateTime dateTime, string? movieTitle)
        {
            _dateTime = dateTime;
            _movieTitle = movieTitle;
        }
    }
}
