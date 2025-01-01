using CinemaApiDomain.Entities;
using CinemaApiDomain.Interfaces;
using CinemaApiInfrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CinemaApiInfrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly CinemaApiDbContext _dbContext;

        public TicketRepository(CinemaApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Ticket ticket)
        {
            await _dbContext.Tickets.AddAsync(ticket);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Ticket>> GetTicketsForUserAsync(string userEmail)
        {
            return await _dbContext.Tickets
                .Where(t => t.UserEmail == userEmail)
                .Include(t => t.Seance)
                .ThenInclude(t => t.Movie)
                .Include(t => t.Seance)
                .ThenInclude(t => t.Hall)
                .Include(t => t.Seats)
                .ToListAsync();
        }
    }
}
