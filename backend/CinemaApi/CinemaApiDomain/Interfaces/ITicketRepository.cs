using CinemaApiDomain.Entities;
using CinemaApiDomain.Entities.Enums;

namespace CinemaApiDomain.Interfaces
{
    public interface ITicketRepository
    {
        Task AddAsync(Ticket ticket);
        Task<IEnumerable<Ticket>> GetTicketsAsync(string? userEmail);
        Task<Ticket?> GetTicketAsync(string reservationCode);
        Task UpdateStateAsync(Ticket ticket);
    }
}
