using CinemaApiDomain.Entities;

namespace CinemaApiApplication.Seat
{
    public class SeatDto
    {
        public int Row { get; set; }
        public int Number { get; set; }
        public bool VIP { get; set; }

        public SeatDto(CinemaApiDomain.Entities.Seat seat)
        {
            Row = seat.Row;
            Number = seat.Number;
            VIP = seat.VIP;
        }
    }
}
