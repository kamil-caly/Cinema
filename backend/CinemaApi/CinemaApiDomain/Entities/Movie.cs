namespace CinemaApiDomain.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int DurationInMin { get; set; }
        public string ImageUrl { get; set; } = default!;
        public List<Seance> Seances { get; set; } = new();
    }
}
