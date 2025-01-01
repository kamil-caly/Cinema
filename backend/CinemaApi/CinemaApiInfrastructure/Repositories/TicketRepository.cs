using CinemaApiDomain.Entities;
using CinemaApiDomain.Interfaces;
using CinemaApiInfrastructure.Persistence;

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
    }
}
