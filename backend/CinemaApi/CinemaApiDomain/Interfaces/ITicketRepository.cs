using CinemaApiDomain.Entities;

namespace CinemaApiDomain.Interfaces
{
    public interface ITicketRepository
    {
        Task AddAsync(Ticket ticket);
    }
}
