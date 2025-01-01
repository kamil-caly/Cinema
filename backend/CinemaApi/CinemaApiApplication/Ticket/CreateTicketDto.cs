using CinemaApiApplication.Seat;

namespace CinemaApiApplication.Ticket
{
    public class CreateTicketDto
    {
        public DateTime SeanceDate { get; set; }
        public List<SeatDto> SeatDtos { get; set; } = new();
    }
}
