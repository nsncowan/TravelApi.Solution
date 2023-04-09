using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelApi.Models;

namespace TravelApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DestinationController : ControllerBase
  {
    private readonly TravelApiContext _db;

    public DestinationController(TravelApiContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<List<Destination>> Get(string country, string city, int rating)
    {
      IQueryable<Destination> query = _db.Destination.AsQueryable();
      if (country != null)
      {
        query = query.Where(entry => entry.Country == country);
      }
      if (city != null)
      {
        query = query.Where(entry => entry.City == city);
      }
      if (rating >= 0)
      {
        query = query.Where(entry => entry.Rating >= rating);
      }
      return await query.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Destination>> GetDestination(int id)
    {
      Destination destination = await _db.Destination.FindAsync(id);

      if (destination == null)
      {
        return NotFound();
      }
      return destination;
    }

    [HttpPost]
    public async Task<ActionResult<Destination>> Post(Destination destination)
    {
      _db.Destination.Add(destination);
      await _db.SaveChangesAsync();
      return CreatedAtAction(nameof(GetDestination), new { id = destination.DestinationId }, destination);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Destination destination)
    {
      if (id != destination.DestinationId)
      {
        return BadRequest();
      }
      _db.Destination.Update(destination);
      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if(!DestinationExists(id))
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

    private bool DestinationExists(int id)
    {
      return _db.Destination.Any(e => e.DestinationId == id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDestination(int id)
    {
      Destination destination = await _db.Destination.FindAsync(id);
      if (destination == null)
      {
        return NotFound();
      }
      _db.Destination.Remove(destination);
      await _db.SaveChangesAsync();
      return NoContent();
    }


  }
}