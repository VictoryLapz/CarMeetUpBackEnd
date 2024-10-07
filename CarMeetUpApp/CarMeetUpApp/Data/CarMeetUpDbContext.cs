using Microsoft.EntityFrameworkCore;

namespace CarMeetUpApp.Data;

public class CarMeetUpDbContext : DbContext

{

    public CarMeetUpDbContext(DbContextOptions<CarMeetUpDbContext> options) : base(options)
    {

    }

    //public DbSet<User> EventListing { get; set; }
    //public DbSet<EventSignUp> EventSignUp { get; set; }
}