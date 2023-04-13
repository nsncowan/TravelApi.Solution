using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelApi.Models;
using System.Linq;
using System.Collections.Generic;

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

    [HttpGet("Sort/{sortOrder}")]
    public async Task<List<Destination>> GetSort(string sortOrder)
    {
        // string reviewCountSortParm = String.IsNullOrEmpty(sortOrder) ? "reviewCount_desc" : "";
        // string ratingSortParm = sortOrder == "Rating" ? "rating_desc" : "Rating";
        var destinations = from s in _db.Destination
                      select s;

        // var destinations =  _db.Destination.Include(x => x.Reviews);

        switch (sortOrder)
        {
            case "ratingAsc":
                destinations = destinations.OrderBy(s => s.Rating);
                break;
            case "ratingDesc":
                destinations = destinations.OrderByDescending(s => s.Rating);
                break;
            case "reviewCountAsc":
                destinations = destinations.OrderBy(s => s.Reviews.Count);
                break;
            default:
                destinations = destinations.OrderByDescending(s => s.Reviews.Count);
                break;
        }
        return await destinations.AsNoTracking().ToListAsync();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<Destination>> GetDestination(int id)
    {
      // Destination destination = await _db.Destination.FindAsync(id);
      Destination destination = await _db.Destination
                                .Include(x => x.Reviews)
                                .FirstOrDefaultAsync(i => i.DestinationId == id);

      if (destination == null)
      {
        return NotFound();
      }
      return destination;
    }

    // //GET: api/Destination/id/Destination
    [HttpGet("{reviewId}/Destination")]
    public async Task<ActionResult<Destination>> GetDestinationFromReview(int reviewId)
    {
      Review review = _db.Review
                          .FirstOrDefault(x => x.ReviewId == reviewId);
      if (review == null)
      {
        return NotFound();
      }

      Destination destination = await _db.Destination.FirstOrDefaultAsync(z => z.DestinationId == review.DestinationId);
      if (destination == null)
      { 
        return NotFound();
      }
      return destination;
    }

    // [HttpGet]
    // public async Task<ActionResult<Destination>> GetRandomDestination()
    // {
    //   // Destination destination = await _db.Destination.Where(destination.DestinationId == SOME RANDOM NUMBER GENERATED ELSEWHERE USING THE MIN AND MAX VALUES AS PARAMETERS)
    //   return Destination;
    // }

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