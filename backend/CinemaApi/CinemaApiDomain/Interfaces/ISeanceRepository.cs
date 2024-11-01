using CinemaApiDomain.Entities;

namespace CinemaApiDomain.Interfaces
{
    public interface ISeanceRepository
    {
        Task<IEnumerable<Seance>> GetAllWithDetailsForGivenDate(DateTime dateTime);
    }
}
