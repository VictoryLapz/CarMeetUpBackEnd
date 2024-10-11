using CarMeetUpApp.Data;
using CarMeetUpApp.Models;
using CarMeetUpApp.Mapper;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarMeetUpApp.Data.Dto;

namespace CarMeetUpApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly CarMeetUpDbContext _carMeetUpDb;

    public UserController(CarMeetUpDbContext carMeetUpDb)
    {
        _carMeetUpDb = carMeetUpDb;
    }

    //register a new user 
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UserDto userDto)
    {
        User users = new User
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName, 
            Email = userDto.Email,
            Bio = userDto.Bio,
            CarInterests = userDto.CarInterests,
        };

        await _carMeetUpDb.AddAsync(users);
        await _carMeetUpDb.SaveChangesAsync();

        return Ok(users); 
    }

    //updates existing user by Id 
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProfile([FromBody] UserDto userDtouUp, int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var users = await _carMeetUpDb.Users.FindAsync(id); 
        if (users == null)
        {
            return NotFound();
        }
        users.FirstName = userDtouUp.FirstName;
        users.LastName = userDtouUp.LastName;
        users.Email = userDtouUp.Email;
        users.Bio = userDtouUp.Bio;
        users.CarInterests = userDtouUp.CarInterests;

        _carMeetUpDb.Users.Update(users);
        await _carMeetUpDb.SaveChangesAsync();

        return Ok(users);
    }

    //deletes existing user by Id
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProfile(int id)
    {
        var deleteUser = _carMeetUpDb.Users.Find(id);

        if (deleteUser == null)
        {
            return NotFound("User Not Found");
        }
        _carMeetUpDb.Users.Remove(deleteUser);
        await _carMeetUpDb.SaveChangesAsync();

        return NoContent(); //204 
    }

}
