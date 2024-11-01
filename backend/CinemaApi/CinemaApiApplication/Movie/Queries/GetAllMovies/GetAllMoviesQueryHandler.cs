using CinemaApiDomain.Interfaces;
using MediatR;

namespace CinemaApiApplication.Movie.Queries
{
    public class GetAllMoviesQueryHandler : IRequestHandler<GetAllMoviesQuery, IEnumerable<MovieDto>>
    {
        private readonly IMovieRepository _movieRepository;

        public GetAllMoviesQueryHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public async Task<IEnumerable<MovieDto>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
        {
            var movies = await _movieRepository.GetAll();
            var dtos = movies.Select(m => new MovieDto(m)).ToList();

            return dtos;
        }
    }
}
