using DataSearcher.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace DataSearcher.Data.Context;

public class TransportRouteContext: DbContext
{
    public virtual DbSet<Route> Routes { get; set; }
    public virtual DbSet<Stop> Stops { get; set; }
    public virtual DbSet<RouteStops> RouteStops { get; set; }
    public virtual DbSet<RouteType> RouteTypes { get; set; }

    public TransportRouteContext(DbContextOptions<TransportRouteContext> options)
        : base(options)
        {
            Database.EnsureCreated();
        }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RouteType>().HasData(new List<RouteType>()
            {
                new RouteType()
                {
                    Id = 0,
                    Name = "Магистральный"
                },
                
                new RouteType()
                {
                    Id = 1,
                    Name = "Районный"
                },
                
                new RouteType()
                {
                    Id = 2,
                    Name = "Социальный"
                }
            }
        );
        
        base.OnModelCreating(modelBuilder);
    }
}