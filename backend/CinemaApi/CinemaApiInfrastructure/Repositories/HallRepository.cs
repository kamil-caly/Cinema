using CinemaApiDomain.Entities;
using CinemaApiDomain.Interfaces;
using CinemaApiInfrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CinemaApiInfrastructure.Repositories
{
    public class HallRepository : IHallRepository
    {
        private readonly CinemaApiDbContext _dbContext;

        public HallRepository(CinemaApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
