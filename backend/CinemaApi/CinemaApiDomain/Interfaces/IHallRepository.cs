using CinemaApiDomain.Entities;

namespace CinemaApiDomain.Interfaces
{
    public interface IHallRepository
    {
        Task<Hall?> GetHallForGivenSeance(DateTime seanceDateTime);
    }
}
