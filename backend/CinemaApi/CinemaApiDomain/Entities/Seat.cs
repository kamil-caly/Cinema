namespace CinemaApiDomain.Entities
{
    public class Seat
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public int Number { get; set; }
        public bool VIP { get; set; }
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; } = default!;
    }
}
