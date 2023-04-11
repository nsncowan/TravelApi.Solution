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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Review>>> GetReview()
        {
            return await _db.Review.ToListAsync();
        }

        // GET: api/Review/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Review>> GetReview(int id)
        {
           Review review = await _db.Review
                    //   .Include(x => x.Destination)
                    //   .Where(x => x.DestinationId == review.DestinationId)
                      .FirstOrDefaultAsync(i => i.ReviewId == id);
            var destination = await _db.Destination.FindAsync(review.DestinationId);

            if (review == null)
            {
                return NotFound();
            }

            return review;
        }

        // PUT: api/Review/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, Review review)
        {
            if (id != review.ReviewId)
            {
                return BadRequest();
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
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _db.Review.FindAsync(id);
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
    }
}
