using CinemaApiDomain.Entities;

namespace CinemaApiApplication.Seat
{
    public class SeatDto
    {
        public int Row { get; set; }
        public int Num { get; set; }
        public bool VIP { get; set; }

        public SeatDto() { }

        public SeatDto(CinemaApiDomain.Entities.Seat seat)
        {
            Row = seat.Row;
            Num = seat.Number;
            VIP = seat.VIP;
        }
    }
}
