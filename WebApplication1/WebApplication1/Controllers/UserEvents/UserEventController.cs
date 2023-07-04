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

        // GET: api/UserEvents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserEvent>> GetUserEvent(int id)
        {
            var userEvent = await _context.UserEvents.FindAsync(id);

            if (userEvent == null)
            {
                return NotFound();
            }

            return userEvent;
        }

        // PUT: api/UserEvents/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserEvent(int id, UserEvent userEvent)
        {
            if (id != userEvent.Id)
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

            return CreatedAtAction(nameof(GetUserEvent), new { id = userEvent.Id }, userEvent);
        }

        // DELETE: api/UserEvents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserEvent(int id)
        {
            var userEvent = await _context.UserEvents.FindAsync(id);
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
