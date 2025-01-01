using CinemaApiApplication.Seat;
using CinemaApiDomain.Entities.Enums;

namespace CinemaApiApplication.Ticket
{
    public class UserTicketDto
    {
        public string ReservationCode { get; set; } = default!;
        public TicketState Status { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string UserEmail { get; set; } = default!;
        public string HallName { get; set; } = default!;
        public string MovieTitle { get; set; } = default!;
        public DateTime SeanceDate { get; set; } = default!;
        public List<SeatDto> SeatDtos { get; set; } = new();
    }
}
