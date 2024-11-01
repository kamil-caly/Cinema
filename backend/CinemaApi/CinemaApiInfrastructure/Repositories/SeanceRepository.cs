﻿using CinemaApiDomain.Entities;
using CinemaApiDomain.Interfaces;
using CinemaApiInfrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CinemaApiInfrastructure.Repositories
{
    public class SeanceRepository : ISeanceRepository
    {
        private readonly CinemaApiDbContext _dbContext;

        public SeanceRepository(CinemaApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Seance>> GetAllWithDetailsForGivenDate(DateTime dateTime)
        {
            return await _dbContext.Seances
                .Include(s => s.Movie)  
                .Include(s => s.Hall)  
                .Where(s => s.Date > DateTime.Now && s.Date.Date == dateTime.Date)
                .OrderBy(s => s.Date)
                .ToListAsync();
        }
    }
}