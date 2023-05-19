using Microsoft.EntityFrameworkCore;
using RouteService.Domain.Entities;

namespace RouteService.Persistence
{
    public class RouteServiceDbContext : DbContext
    {
        public DbSet<Ride> Rides { get; set; }
        public DbSet<Route> Routes { get; set; }
        public RouteServiceDbContext(DbContextOptions<RouteServiceDbContext> options) : base(options) { }
    }
}
