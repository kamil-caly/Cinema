using CinemaApiDomain.Entities;

namespace CinemaApiApplication.Movie
{
    public class MovieDto
    {
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int DurationInMin { get; set; }
        public string ImageUrl { get; set; } = default!;

        public MovieDto(CinemaApiDomain.Entities.Movie movie)
        {
            Title = movie.Title;
            Description = movie.Description;
            DurationInMin = movie.DurationInMin;
            ImageUrl = movie.ImageUrl;
        }
    }
}
