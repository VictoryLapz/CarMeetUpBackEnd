using CarMeetUpApp.Data;
using CarMeetUpApp.Models;
using Microsoft.AspNetCore.Mvc;
using CarMeetUpApp.Mapper;
using CarMeetUpApp.Data.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace CarMeetUpApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventController : ApiBaseController
{
    private readonly CarMeetUpDbContext _carmeetupDB;

    public EventController(CarMeetUpDbContext carmeetupDbContext)
    {
        _carmeetupDB = carmeetupDbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEvents()
    {
        var events = await _carmeetupDB.Events.ToListAsync();

        var eventDtos = events.Select(events => events.ToDto()).ToList();

        return Ok(eventDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEventById(int id)
    {
        var events = await _carmeetupDB.Events.FindAsync(id);

        if (events == null)
        {
            return NotFound();
        }

        return Ok(events.ToDto());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
        var events = _carmeetupDB.Events.Find(id);

        if (events == null)
        {
            return NotFound();
        }

        _carmeetupDB.Events.Remove(events);
        await _carmeetupDB.SaveChangesAsync();

        return NoContent();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] EventDto eventDto)
    {

        Event newEvent = new Event
        {
            Title = eventDto.Title,
            Location = eventDto.Location,
            Date = eventDto.Date,
            Description = eventDto.Description,
            Capacity = eventDto.Capacity,
            CarId = eventDto.CarId, 
            CreatedBy = GetCurrentUserID(),
        };

        _carmeetupDB.Events.Add(newEvent);
        await _carmeetupDB.SaveChangesAsync();

        return Ok(newEvent);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent([FromBody,] EventDto dto, int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var events = await _carmeetupDB.Events.FindAsync(id);

        if (events == null)
        {
            return NotFound();
        }


        events.Title = dto.Title;
        events.Location = dto.Location;
        events.Date = dto.Date;
        events.Description = dto.Description;
        events.Capacity = dto.Capacity;
        events.UpdatedBy = GetCurrentUserID();

        _carmeetupDB.Events.Update(events);
        await _carmeetupDB.SaveChangesAsync();

        return Ok(events.ToDto());
    }


    [HttpGet("SearchbyMake")] 
    public async Task<IActionResult> SearchByCarMake(string make)
    {
        // Check if the make parameter is provided
        if (string.IsNullOrEmpty(make))
        {
            return BadRequest("Car make is required.");
        }

 
        var events = await _carmeetupDB.Events
            .Include(e => e.CarSearch) 
            .Where(e => e.CarSearch.Make.ToLower() == make.ToLower()) 
            .ToListAsync();

     
        if (events == null || events.Count == 0)
        {
            return NotFound("No events found for the specified car make.");
        }

        return Ok(events.Select(e => e.ToDto())); 
    }
}
