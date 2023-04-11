using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelApi.Models
{
  public class Destination
  {
    public int DestinationId { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    [Required]
    [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
    public int Rating { get; set; }

    public List<Review> Reviews { get; set; }
      // create a review object (properties includes, author, (maybe date?))
      // review object with be stored in a list in each Destination
          //Create Review
          //Link to a Destination
      // let's attempt to access reviews with dot notation for PUT request
      // stretch goal add authentication
  }
}