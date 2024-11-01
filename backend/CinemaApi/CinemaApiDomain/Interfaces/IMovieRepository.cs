using CinemaApiDomain.Entities;

namespace CinemaApiDomain.Interfaces
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAll();
    }
}
