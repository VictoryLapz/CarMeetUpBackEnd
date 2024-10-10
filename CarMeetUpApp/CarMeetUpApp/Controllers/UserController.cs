using CarMeetUpApp.Data;
using CarMeetUpApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> Register([FromBody] User user)
    {
        User newUser = new User();

        newUser.FirstName = user.FirstName;
        newUser.LastName = user.LastName;
        newUser.Email = user.Email;
        newUser.Bio = user.Bio;
        newUser.CarInterests = user.CarInterests;


        _carMeetUpDb.AddAsync(newUser);
        _carMeetUpDb.SaveChanges();

        return Ok(newUser); //CreatedAtAction() for later 
    }

    //updates existing user by Id 
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProfile(int Id, [FromBody] User user)
    {
        var userUpdate = _carMeetUpDb.Users.Find(Id); 
        if (userUpdate == null)
        {
            return NotFound("User Not Found");
        }

        userUpdate.FirstName = user.FirstName;
        userUpdate.LastName = user.LastName;
        userUpdate.Email = user.Email;
        userUpdate.Bio = user.Bio;
        userUpdate.CarInterests = user.CarInterests;

        _carMeetUpDb.Users.Update(userUpdate);
        _carMeetUpDb.SaveChangesAsync();

        return Ok(userUpdate);
    }
    //deletes existing user by Id
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProfile(int Id)
    {
        var deleteUser = _carMeetUpDb.Users.Find(Id);

        if (deleteUser == null)
        {
            return NotFound("User Not Found");
        }
        _carMeetUpDb.Users.Remove(deleteUser);
        _carMeetUpDb.SaveChangesAsync();

        return NoContent(); //204 
    }

}
