using CarMeetUpApp.Data;
using CarMeetUpApp.Data.Dto;
using CarMeetUpApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarMeetUpApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventSignUpController : ControllerBase
{
    private CarMeetUpDbContext _carmeetupDbContext;

    public EventSignUpController(CarMeetUpDbContext carmeetupDbContext)
    {
        _carmeetupDbContext = carmeetupDbContext;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetEventSignUpsByUserId(int userId)
    {
        
        IQueryable<EventSignUp> userEventSignUps = _carmeetupDbContext.EventSignUps.Where(x => x.UserId == userId);


        var result = from signUp in userEventSignUps
                     join event_ in _carmeetupDbContext.Events on signUp.EventId equals event_.EventId into UserEvents
                     from m in UserEvents.DefaultIfEmpty()
                     select new EventSignUpDto
                     {
                         EventId = signUp.EventId,
                         EventName = m != null ? m.Title : "Unknown Event",
                         EventDescription = m != null ? m.Description : "No Description",
                         EventLocation = m != null ? m.Location : "Unknown Location",
                         EventTime = m != null ? m.Date : DateTime.MinValue
                     };

        return Ok(await result.ToListAsync());
    }


    [HttpPost]
    public async Task<IActionResult> CreateEventSignUp([FromBody] EventSignUp eventSignUp)
    {

        if (!ModelState.IsValid)
        {

            return BadRequest(ModelState);
        }


        var newSignUp = new EventSignUp
        {
            UserId = eventSignUp.UserId,
            EventId = eventSignUp.EventId
        };

        _carmeetupDbContext.Add(newSignUp);
        await _carmeetupDbContext.SaveChangesAsync();

        return Ok(newSignUp);
    }


    [HttpDelete("userId={userId}/eventId={eventId}")]
    public async Task<IActionResult> DeleteEventSignUp(int userId, int eventId)
    {
        var signUpEntity = await _carmeetupDbContext.EventSignUps.FirstOrDefaultAsync(x => x.UserId == userId && x.EventId == eventId);

        if (signUpEntity == null)
        {
            return NotFound();
        }

        _carmeetupDbContext.EventSignUps.Remove(signUpEntity);
        await _carmeetupDbContext.SaveChangesAsync();

        return NoContent();
    }
}




