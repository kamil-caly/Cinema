using CinemaApiDomain.Entities.Enums;

namespace CinemaApiDomain.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public string ReservationCode { get; set; } = default!;
        public TicketState Status { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string UserEmail { get; set; } = default!;
        public int SeanceId { get; set; }
        public Seance Seance { get; set; } = default!;
        public List<Seat> Seats { get; set; } = new();
    }
}
