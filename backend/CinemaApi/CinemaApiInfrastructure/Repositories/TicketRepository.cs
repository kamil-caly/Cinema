using CinemaApiDomain.Entities;
using CinemaApiDomain.Entities.Enums;
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

        public async Task<Ticket?> GetTicketAsync(string reservationCode)
        {
            return await _dbContext.Tickets
                .Include(t => t.Seance)
                .FirstOrDefaultAsync(t => t.ReservationCode == reservationCode);
        }

        public async Task<IEnumerable<Ticket>> GetTicketsAsync(string? userEmail)
        {
            return await _dbContext.Tickets
                .Where(t => userEmail != null ? t.UserEmail == userEmail : true)
                .Include(t => t.Seance)
                .ThenInclude(t => t.Movie)
                .Include(t => t.Seance)
                .ThenInclude(t => t.Hall)
                .Include(t => t.Seats)
                .ToListAsync();
        }

        public async Task UpdateStateAsync(Ticket ticket)
        { 
            _dbContext.Tickets.Update(ticket);
            await _dbContext.SaveChangesAsync();
        }
    }
}
