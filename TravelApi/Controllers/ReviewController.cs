using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelApi.Models;

namespace TravelApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ReviewController : ControllerBase
  {
    private readonly TravelApiContext _db;

    public ReviewController(TravelApiContext db)
    {
        _db = db;
    }

    // GET: api/Review
    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<Review>>> GetReview()
    // {
      
    //   return await _db.Review.ToListAsync();
    // }

/* 
    [HttpGet]
    public async Task<List<Review>> Get(string country, string city)
    {
      IQueryable<Review> query = _db.Review
                                    // .Include(x => x.Destination).ThenInclude(y => y.City)
                                    // .Include(b => b.Destination.Select(p => p.City))
                                    // .Include(x=>x.DestinationId)
                                    .AsQueryable();
      // Find the destination being searched
      // Write down the destination ID
      // Look in the Reviews database for reviews that have the ID

      if (country != null)
      {
        //Load list of destinations where country matches
        //Load list of reviews where destination ID matches the destinations we just found
        query = query.Where(entry => entry.DestinationId.Country == country);
      }
      if (city != null)
      {
        query = query.Where(entry => entry.DestinationId.City == city);
      }
      return await query.ToListAsync();
    }

 */   
    // GET: api/Review/5
    [HttpGet("{reviewId}")]
    public async Task<ActionResult<Review>> GetReview(int reviewId)
    {
      Review review = await _db.Review
                .FirstOrDefaultAsync(i => i.ReviewId == reviewId);
      if (review == null)
      {
          return NotFound();
      }
      return review;
    }

    // PUT: api/Review/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutReview(int id, Review review, string user_name)
    {
      if (id != review.ReviewId)
      {
          return BadRequest();
      }
      if (review.Author != user_name)
      {
        return Unauthorized();
      }
      _db.Entry(review).State = EntityState.Modified;
      try
      {
          await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
          if (!ReviewExists(id))
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

    // POST: api/Review
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Review>> PostReview(Review review)
    {
      var destination = await _db.Destination.FindAsync(review.DestinationId);
      if (destination == null)
      {
          return NotFound();
      }
      _db.Review.Add(review);
      await _db.SaveChangesAsync();

      return CreatedAtAction("GetReview", new { id = review.ReviewId }, review);
    }

    // DELETE: api/Review/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReview(int id, string user_name)
    {
      var review = await _db.Review.FindAsync(id);
      if (review.Author != user_name)
      {
        return Unauthorized();
      }
      if (review == null)
      {
          return NotFound();
      }
      _db.Review.Remove(review);
      await _db.SaveChangesAsync();
      return NoContent();
    }

    private bool ReviewExists(int id)
    {
      return _db.Review.Any(e => e.ReviewId == id);
    }

    // Get: api/Review/5/Reviews

    [HttpGet("{destinationId}/Reviews")]
    public async Task<ActionResult<List<Review>>> GetDestinationReviews(int destinationId)
    {
      // Destination destination = await _db.Destination
      //                 .Include(x => x.Reviews)
      //                 .FirstOrDefaultAsync(i => i.DestinationId == destinationId);

      // Pull list of Reviews
      // Only get the ones where the review property matches destinationId
      IQueryable<Review> query = _db.Review
                                    .Where(x=> x.DestinationId == destinationId)
                                    .AsQueryable();
      // Return           
      if (query == null || query.Count() == 0)
      {
        return NotFound();
      }
      return await query.ToListAsync();
    }
  }
}
