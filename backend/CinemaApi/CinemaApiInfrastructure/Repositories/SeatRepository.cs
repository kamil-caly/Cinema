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

        public async Task<IEnumerable<Seat>> GetSeatsForGivenSeanceDate(DateTime seanceDate)
        {
            var seance = await _dbContext.Seances
                .Include(s => s.Tickets)
                .ThenInclude(t => t.Seats)
                .FirstOrDefaultAsync(s => s.Date == seanceDate);

            return seance?.Tickets.SelectMany(t => t.Seats) ?? [];
        }
    }
}
