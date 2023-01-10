
using Hotel_Listings.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Listings.Data
{
    public class DatabaseContext : DbContext {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        { }

        
        public DbSet<Country> Countries { get; set; }
        public DbSet <Hotel> Hotels{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();

        }
    }
}
