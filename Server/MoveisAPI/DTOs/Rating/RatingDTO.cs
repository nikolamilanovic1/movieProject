using System.ComponentModel.DataAnnotations;

namespace MoveisAPI.DTOs.Rating
{
    public class RatingDTO
    {
        [Range(1,5)]
        public int Rate { get; set; }
        public int MovieId { get; set; }
    }
}
