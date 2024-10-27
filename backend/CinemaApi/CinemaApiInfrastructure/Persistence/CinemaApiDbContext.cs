﻿using CinemaApiDomain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CinemaApiInfrastructure.Persistence
{
    public class CinemaApiDbContext : DbContext
    {
        public CinemaApiDbContext(DbContextOptions<CinemaApiDbContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Seance> Seances { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Seat> Seats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Seances)
                .WithOne(s => s.Movie)
                .HasForeignKey(s => s.MovieId);

            modelBuilder.Entity<Hall>()
                .HasMany(h => h.Seances)
                .WithOne(s => s.Hall)
                .HasForeignKey(s => s.HallId);

            modelBuilder.Entity<Seance>()
                .HasMany(s => s.Tickets)
                .WithOne(t => t.Seance)
                .HasForeignKey(t => t.SeanceId);

            modelBuilder.Entity<Ticket>()
                .HasMany(t => t.Seats)
                .WithOne(s => s.Ticket)
                .HasForeignKey(s => s.TicketId);
        }
    }
}
