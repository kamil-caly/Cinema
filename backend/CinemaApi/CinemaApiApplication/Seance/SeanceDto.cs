using CinemaApiDomain.Entities;

namespace CinemaApiApplication.Seance
{
    public class SeanceDto
    {
        public DateTime SeanceDate { get; set; }
        public string? MovieTitle { get; set; }
        public string? MovieDescription { get; set; }
        public int? MovieDurationInMin { get; set; }
        public string? MovieImageUrl { get; set; }
        public string? HallName { get; set; }
        public int? HallCapacity { get; set; }

        public SeanceDto()
        {
        }

        public SeanceDto(CinemaApiDomain.Entities.Seance seance, CinemaApiDomain.Entities.Movie? movie, CinemaApiDomain.Entities.Hall? hall)
        {
            SeanceDate = seance.Date;
            MovieTitle = movie?.Title;
            MovieDescription = movie?.Description;
            MovieDurationInMin = movie?.DurationInMin;
            MovieImageUrl = movie?.ImageUrl;
            HallName = hall?.Name;   
            HallCapacity = hall?.Capacity;
        }
    }
}
