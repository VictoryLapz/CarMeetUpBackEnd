using CarMeetUpApp.Data;
using CarMeetUpApp.Data.Dto;
using CarMeetUpApp.Mapper;

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

    //search cars in Db
    [HttpGet]
    public async Task<IActionResult> SearchCars([FromQuery] string car)
    {
        IQueryable<Car> query = _carMeetUpDb.Cars;
        
        if (!string.IsNullOrEmpty(car))
        {
            query = query.Where(c => c.Make.Contains(car) || c.Model.Contains(car));
        }
        
        return Ok(await query.ToListAsync());
    }

    //add a car
    [HttpPost]
    public async Task<IActionResult> AddCar([FromBody] CarDto carDto)
    {
        Car cars = new Car
        {
            Make = carDto.Make,
            Model = carDto.Model,
            Year = carDto.Year,
            CarType = carDto.CarType, //enum1
            FuelType = carDto.FuelType, //enum2
            TransmissionType = carDto.TransmissionType, //enum3
            Color = carDto.Color,
        };

        await _carMeetUpDb.AddAsync(cars);
        await _carMeetUpDb.SaveChangesAsync();

        return Ok(cars);
    }

    //update an existing car by ID
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCar([FromBody] CarDto carDtoUp, int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var cars = await _carMeetUpDb.Cars.FindAsync(id);
        
        if (cars == null)
        {
            return NotFound();
        }
        cars.Make = carDtoUp.Make;
        cars.Model = carDtoUp.Model;
        cars.Year = carDtoUp.Year;
        cars.CarType = carDtoUp.CarType;
        cars.FuelType = carDtoUp.FuelType;
        cars.TransmissionType = carDtoUp.TransmissionType;
        cars.Color = carDtoUp.Color;

        _carMeetUpDb.Cars.Update(cars);
        await _carMeetUpDb.SaveChangesAsync();

        return Ok(cars);
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
        await _carMeetUpDb.SaveChangesAsync();

        return NoContent(); //204 
    }


}
