using CinemaApiDomain.Entities;
using CinemaApiDomain.Interfaces;
using CinemaApiInfrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CinemaApiInfrastructure.Repositories
{
    public class HallRepository : IHallRepository
    {
        private readonly CinemaApiDbContext _dbContext;

        public HallRepository(CinemaApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Hall?> GetHallForGivenSeance(DateTime seanceDateTime)
        {
            return await _dbContext.Halls
                .Where(h => h.Seances.Any(s => s.Date == seanceDateTime))
                .Include(h => h.Seances.Where(s => s.Date == seanceDateTime))
                    .ThenInclude(s => s.Movie)
                .Include(h => h.Seances.Where(s => s.Date == seanceDateTime))
                    .ThenInclude(s => s.Tickets)
                    .ThenInclude(t => t.Seats)
                .FirstOrDefaultAsync();
        }
    }
}
