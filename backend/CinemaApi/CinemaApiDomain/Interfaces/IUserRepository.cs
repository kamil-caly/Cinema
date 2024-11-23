﻿using CinemaApiDomain.Entities;

namespace CinemaApiDomain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);
    }
}
