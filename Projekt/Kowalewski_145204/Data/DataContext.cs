using Kowalewski_145204.Models;
using Microsoft.EntityFrameworkCore;

namespace Kowalewski_145204.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Producent> Producenci { get; set; }
        public DbSet<Samochod> Samochody { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producent>().ToTable("Producenci");
            modelBuilder.Entity<Samochod>().ToTable("Samochody");
        }
    }
}
