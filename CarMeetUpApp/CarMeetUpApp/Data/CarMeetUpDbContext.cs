using CarMeetUpApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CarMeetUpApp.Data;

public class CarMeetUpDbContext : DbContext

{

    public CarMeetUpDbContext(DbContextOptions<CarMeetUpDbContext> options) : base(options) { }
    
    public virtual DbSet<Car> Cars { get; set; }
    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Event> Events { get; set; }
    public virtual DbSet<EventSignUp> EventSignUps { get; set; }

}

 