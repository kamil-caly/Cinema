using CinemaApiDomain.Entities;

namespace CinemaApiDomain.Interfaces
{
    public interface ISeatRepository
    {
        Task<IEnumerable<Seat>> GetSeatsForGivenSeanceDate(DateTime seanceDate);
    }
}
