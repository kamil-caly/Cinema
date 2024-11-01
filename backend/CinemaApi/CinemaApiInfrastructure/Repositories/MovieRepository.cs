using CinemaApiDomain.Entities;
using CinemaApiDomain.Interfaces;
using CinemaApiInfrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CinemaApiInfrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly CinemaApiDbContext _dbContext;

        public MovieRepository(CinemaApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Movie>> GetAll()
        {
            return await _dbContext.Movies.ToListAsync();
        }
    }
}
