using CarMeetUpApp.Data;
using CarMeetUpApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarMeetUpApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarController : ControllerBase
{
    private readonly CarMeetUpDbContext _carMeetUpDb;

    public CarController(CarMeetUpDbContext carMeetUpDb)
    {
        _carMeetUpDb = carMeetUpDb;
    }

    //search ALL cars in Db
    [HttpGet]
    public async Task<IActionResult> SearchCars([FromQuery] string Car)
    {
        IQueryable<Car> query = _carMeetUpDb.Cars;

        return Ok(await query.ToListAsync());
    }

    //add a car
    [HttpPost]
    public async Task<IActionResult> AddCar([FromBody] Car car)
    {
        Car newCar = new Car();

        newCar.Make = car.Make;
        newCar.Model = car.Model;
        newCar.Year = car.Year;
        newCar.CarType = car.CarType; //enum1
        newCar.FuelType = car.FuelType; //enum2
        newCar.TransmissionType = car.TransmissionType; //enum3
        newCar.VIN = car.VIN;
        newCar.Color = car.Color;

        _carMeetUpDb.AddAsync(newCar);
        _carMeetUpDb.SaveChangesAsync();

        var createdCars = _carMeetUpDb.Cars.ToList();

        return Ok(newCar); //CreatedAtAct
    }

    //update an existing car by ID

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCar(int Id, [FromBody] Car car)
    {
        var carUpdateCar = _carMeetUpDb.Cars.Find(Id);
        if (carUpdateCar == null)
        {
            return NotFound("Car Not Found");
        }

        carUpdateCar.Make = car.Make;
        carUpdateCar.Model = car.Model;
        carUpdateCar.Year = car.Year;
        carUpdateCar.CarType = car.CarType; //enum1
        carUpdateCar.FuelType = car.FuelType; //enum2
        carUpdateCar.TransmissionType = car.TransmissionType; //enum3
        carUpdateCar.VIN = car.VIN;
        carUpdateCar.Color = car.Color;


        _carMeetUpDb.Cars.Update(carUpdateCar);
        _carMeetUpDb.SaveChangesAsync();

        return Ok(carUpdateCar);
    }

    //delete a car by ID

    [HttpDelete("{id}")]

    public async Task<IActionResult> DeleteCar(int Id)
    {
        var deleteCar = _carMeetUpDb.Users.Find(Id);

        if (deleteCar == null)
        {
            return NotFound("Car Not Found");
        }
        _carMeetUpDb.Users.Remove(deleteCar);
        _carMeetUpDb.SaveChangesAsync();

        return NoContent(); //204 
    }


}
