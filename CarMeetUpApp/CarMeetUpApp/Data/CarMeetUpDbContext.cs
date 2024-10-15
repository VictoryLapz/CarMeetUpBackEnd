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
    
    //in order to search the event model/db for any cars that are in the event, we needed to create this explicit relationship between car and event models
    protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
            modelBuilder.Entity<Event>()
           .HasOne(e => e.CarSearch)  // has one car MAKE associated with the event
           .WithMany()                     // this allows it so it can be part of many events
           .HasForeignKey(e => e.CarId);   // this is the connector
      }

    }



