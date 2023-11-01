using Microsoft.EntityFrameworkCore;
using MontyHall.Model;

namespace MontyHall.Data;

public class MontyHallContext : DbContext
  {
        public MontyHallContext(
            DbContextOptions options) : base(options)
        {
        }

        public DbSet<Simulation> Simulations { get; set; }
        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Simulation>().HasMany(s => s.Games);
        }

  }