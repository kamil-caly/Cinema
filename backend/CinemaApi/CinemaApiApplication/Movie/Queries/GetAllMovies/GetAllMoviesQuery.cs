using MediatR;

namespace CinemaApiApplication.Movie.Queries
{
    public class GetAllMoviesQuery : IRequest<IEnumerable<MovieDto>>
    {
    }
}
