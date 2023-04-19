using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TravelApi.Models
{
  public class Review
  {
    public int ReviewId { get; set; }
    public string Text { get; set; }
    public string Author { get; set; }
    public int DestinationId { get; set; }

    [JsonIgnore]
    public Destination Destination { get; set; }

    [Required]
    [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
    public int Rating { get; set; }
  }
}