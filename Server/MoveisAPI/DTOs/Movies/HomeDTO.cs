namespace MoveisAPI.DTOs
{
    public class HomeDTO
    {
        public List<MovieDTO>? InTheatres { get; set; }
        public List<MovieDTO> UpcomingReleases { get; set; }
    }
}
