using Microsoft.EntityFrameworkCore;

namespace TravelApi.Models
{
  public class TravelApiContext : DbContext
  {
    public DbSet<Destination> Destination { get; set; }

    public TravelApiContext(DbContextOptions<TravelApiContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Destination>()
        .HasData(
          new Destination { DestinationId = 1, Country = "Japan", City = "Tokyo", Rating = 5, Review = "" },
          new Destination { DestinationId = 2, Country = "India", City = "Mumbai", Rating = 5, Review = "" },
          new Destination { DestinationId = 3, Country = "France", City = "Paris", Rating = 5, Review = "" },
          new Destination { DestinationId = 4, Country = "England", City = "London", Rating = 5, Review = "" },
          new Destination { DestinationId = 5, Country = "Australia", City = "Sydney", Rating = 5, Review = "" }
        );
    }
  }
}