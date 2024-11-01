using CinemaApiDomain.Entities;

namespace CinemaApiDomain.Interfaces
{
    public interface ISeanceRepository
    {
        Task<IEnumerable<Seance>> GetAllWithDetailsForGivenArgs(DateTime dateTime, string? movieTitle);
    }
}
