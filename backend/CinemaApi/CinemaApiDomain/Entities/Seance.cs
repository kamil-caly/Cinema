namespace CinemaApiDomain.Entities
{
    public class Seance
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; } = default!;
        public int HallId { get; set; }
        public Hall Hall { get; set; } = default!;
        public List<Ticket> Tickets { get; set; } = new();
    }
}
