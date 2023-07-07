using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventHorizonBackend.Models;
using EventHorizonBackend.Data;

namespace EventHorizonBackend.Controllers.UserEvents
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserEventsController : ControllerBase
    {
        private readonly EventHorizonDbContext _context;

        public UserEventsController(EventHorizonDbContext context)
        {
            _context = context;
        }

        // GET: api/UserEvents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserEvent>>> GetUserEvents()
        {
            return await _context.UserEvents.ToListAsync();
        }

        // GET: api/UserEvents/userId/eventId
        [HttpGet("{userId}/{eventId}")]
        public async Task<ActionResult<UserEvent>> GetUserEvent(int userId, int eventId)
        {
            var userEvent = await _context.UserEvents.FindAsync(userId, eventId);

            if (userEvent == null)
            {
                return NotFound();
            }

            return userEvent;
        }


        // PUT: api/UserEvents/userId/eventId
        [HttpPut("{userId}/{eventId}")]
        public async Task<IActionResult> PutUserEvent(int userId, int eventId, UserEvent userEvent)
        {
            if (userId != userEvent.UserId || eventId != userEvent.EventId)
            {
                return BadRequest();
            }

            _context.Entry(userEvent).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/UserEvents
        [HttpPost]
        public async Task<ActionResult<UserEvent>> PostUserEvent(UserEvent userEvent)
        {
            _context.UserEvents.Add(userEvent);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserEvent), new { userId = userEvent.UserId, eventId = userEvent.EventId }, userEvent);
        }

        // DELETE: api/UserEvents/userId/eventId
        [HttpDelete("{userId}/{eventId}")]
        public async Task<IActionResult> DeleteUserEvent(int userId, int eventId)
        {
            var userEvent = await _context.UserEvents.FindAsync(userId, eventId);
            if (userEvent == null)
            {
                return NotFound();
            }

            _context.UserEvents.Remove(userEvent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
