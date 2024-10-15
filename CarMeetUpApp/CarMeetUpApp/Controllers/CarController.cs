using CarMeetUpApp.Data;
using CarMeetUpApp.Models;
using CarMeetUpApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CarMeetUpApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarMeetUpDbContext _context;
        private readonly ApiService _apiService;

        public CarController(CarMeetUpDbContext context, ApiService apiService)
        {
            _context = context;
            _apiService = apiService;
        }

        [HttpGet("byid/{id}")]
        public async Task<ActionResult<Car>> GetCarById(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null) return NotFound();
            return car;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return await _context.Cars.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Car>> CreateCar(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCarById), new { id = car.Id }, car);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(int id, [FromBody] Car car)
        {
      
            if (car == null)
            {
                return BadRequest("Car data is required.");
            }


            var existingCar = await _context.Cars.FindAsync(id);
            if (existingCar == null)
            {
                return NotFound("Car not found.");
            }

            // for some reason i had to map this our in order to make the put request, otherwise i kept returning a bad error. 
            // seems like this is the only way i can get it to work. open to suggestions here.
            existingCar.CityMpg = car.CityMpg;
            existingCar.Class = car.Class;
            existingCar.CombinationMpg = car.CombinationMpg;
            existingCar.Cylinders = car.Cylinders;
            existingCar.Displacement = car.Displacement;
            existingCar.Drive = car.Drive;
            existingCar.FuelType = car.FuelType;
            existingCar.HighwayMpg = car.HighwayMpg;
            existingCar.Make = car.Make;
            existingCar.Model = car.Model;
            existingCar.Transmission = car.Transmission;
            existingCar.Year = car.Year;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null) return NotFound();
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("external/{make}")]
        public async Task<IActionResult> GetCarFromExternalAPI(string make)
        {
            try
            {
                // had to do a try/catch to try and see if the specific make is even in the external api
                var cars = await _apiService.GetCarDataAsync(make);

                // quick check just to make sure
                if (cars == null || !cars.Any())
                {
                    return NotFound("No cars found from the external API.");
                }

                //since the api has multipe car that it can return we can save them all with a loop
                foreach (var car in cars)
                {

                    var existingCar = await _context.Cars.FirstOrDefaultAsync(c => c.Id == car.Id);
                    if (existingCar == null)
                    {
                        _context.Cars.Add(car);
                    }
                    else
                    {
                        
                        existingCar.Make = car.Make;
                        existingCar.Model = car.Model;
                        existingCar.Year = car.Year;
                   
                    }
                }

                await _context.SaveChangesAsync();

                return Ok(cars); 
            }
            catch (HttpRequestException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        // added this so when we tackle frontend the user can have a "drop down to select a car make"
        [HttpGet("CarMakes")]
        public async Task<ActionResult<IEnumerable<string>>> GetCarMakes()
        {
            var carMakes = await _context.Cars.Select(c => c.Make).Distinct().ToListAsync();
            return Ok(carMakes);
        }
    }
}
    
