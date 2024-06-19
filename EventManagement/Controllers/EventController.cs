using EventManagement.Models;
using EventManagement.DataConnection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace EventManagement.Controllers
{
  [Route("api/[controller]")]
  [ApiController, Authorize]
  public class EventController : ControllerBase
  {
    private readonly DbContextClass _context;

    public EventController(DbContextClass context)
    {
      _context = context;
    }

    // GET: api/Events
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Event>>> GetEvents()
    {
      return await _context.Events.ToListAsync();
    }

    // GET: api/Events/5
    [HttpGet("{id}")]
    [Authorize(Roles = "1")]
    public async Task<ActionResult<Event>> GetEvent(int id)
    {
      var @event = await _context.Events.FindAsync(id);

      if (@event == null)
      {
        return NotFound();
      }

      return @event;
    }

    // PUT: api/Events/5
    [HttpPut("{id}")]
    [Authorize(Roles = "1")]
    public async Task<IActionResult> PutEvent(int id, Event @event)
    {
      if (id != @event.Id)
      {
        return BadRequest();
      }

      @event.Updated_At = DateTime.UtcNow;
      _context.Entry(@event).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!EventExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    // POST: api/Events
    [HttpPost]
    public async Task<ActionResult<Event>> PostEvent(Event @event)
    {
      @event.Created_At = DateTime.UtcNow;
      _context.Events.Add(@event);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetEvent", new { id = @event.Id }, @event);
    }

    // DELETE: api/Events/5
    [HttpDelete("{id}")]
    [Authorize(Roles = "1")]
    public async Task<IActionResult> DeleteEvent(int id)
    {
      var @event = await _context.Events.FindAsync(id);
      if (@event == null)
      {
        return NotFound();
      }

      _context.Events.Remove(@event);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool EventExists(int id)
    {
      return _context.Events.Any(e => e.Id == id);
    }
  }
}
