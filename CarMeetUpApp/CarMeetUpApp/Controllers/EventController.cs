using CarMeetUpApp.Data;
using CarMeetUpApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarMeetUpApp.Mapper;

using CarMeetUpApp.Models;
using CarMeetUpApp.Data.Dto;
using Microsoft.EntityFrameworkCore;

namespace CarMeetUpApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private CarMeetUpDbContext _carmeetupDB;

        public EventController(CarMeetUpDbContext carmeetupDbContext)
        {
            _carmeetupDB = carmeetupDbContext;
        }

        // GET: api/Event
        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _carmeetupDB.Events.ToListAsync();

            var eventDtos = events.Select(events => events.ToDto()).ToList();

            return Ok(eventDtos);
        }

        // GET: api/Event/2
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

        // DELETE: api/Event/2
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

        // POST: api/Event
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


            };

            _carmeetupDB.Events.Add(newEvent);
            await _carmeetupDB.SaveChangesAsync();

            return Ok(newEvent);
        }

        // PUT: api/Event/2
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



            _carmeetupDB.Events.Update(events);
            await _carmeetupDB.SaveChangesAsync();

            return Ok(events.ToDto());
        }
    }
}
