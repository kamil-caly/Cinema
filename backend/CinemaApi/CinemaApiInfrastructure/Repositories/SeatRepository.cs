using CinemaApiDomain.Entities;
using CinemaApiDomain.Interfaces;
using CinemaApiInfrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CinemaApiInfrastructure.Repositories
{
    public class SeatRepository : ISeatRepository
    {
        private readonly CinemaApiDbContext _dbContext;

        public SeatRepository(CinemaApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Seat>?> GetAllForGivenSeance(DateTime seanceDateTime)
        {
            //return await _dbContext.Seances
            //    .Where(s => s.Date == seanceDateTime)
            //    .SelectMany(s => s.Tickets.SelectMany(t => t.Seats))
            //    .ToListAsync();

            // inna składnia zapytania
            return await(
                  from s in _dbContext.Seances
                  where s.Date == seanceDateTime
                  from t in s.Tickets
                  from seat in t.Seats
                  select seat
              ).ToListAsync();
        }
    }
}
