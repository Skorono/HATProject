using DataSearcher.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace DataSearcher.Data.Context;

public class TransportRouteContext : DbContext
{
    public TransportRouteContext(DbContextOptions<TransportRouteContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    public virtual DbSet<Route> Routes { get; set; }
    public virtual DbSet<Stop> Stops { get; set; }
    public virtual DbSet<RouteStops> RouteStops { get; set; }
    public virtual DbSet<RouteType> RouteTypes { get; set; }
    public virtual DbSet<TransportType> TransportTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RouteType>().HasData(new List<RouteType>
            {
                new() { Id = RouteType.Types.Mainline, Name = nameof(RouteType.Types.Mainline) },
                new() { Id = RouteType.Types.Local, Name = nameof(RouteType.Types.Local) },
                new() { Id = RouteType.Types.Social, Name = nameof(RouteType.Types.Social) },
                new() { Id = RouteType.Types.Diametrical, Name = nameof(RouteType.Types.Diametrical) },
                new() { Id = RouteType.Types.Express, Name = nameof(RouteType.Types.Express) }
            }
        );

        modelBuilder.Entity<TransportType>().HasData(new List<TransportType>
        {
            new() { Id = TransportType.Types.Bus, Name = nameof(TransportType.Types.Bus) },
            new() { Id = TransportType.Types.Tramway, Name = nameof(TransportType.Types.Tramway) },
            new() { Id = TransportType.Types.Trolleybus, Name = nameof(TransportType.Types.Trolleybus) }
        });

        base.OnModelCreating(modelBuilder);
    }
}