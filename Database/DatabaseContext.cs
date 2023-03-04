using Microsoft.EntityFrameworkCore;

namespace Carpool.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Ride> Rides { get; set; }

        public DbSet<Stop> Stops { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
    }
}