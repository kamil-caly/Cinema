using CinemaApiApplication.Seat;

namespace CinemaApiApplication.Hall
{
    public class HallDto
    {
        public int Capacity { get; set; }
        public string HallName { get; set; }
        public string MovieTitle { get; set; }
        public int MovieDurationInMin { get; set; }
        public string MovieImageUrl { get; set; }
        public DateTime SeanceDate { get; set; }
        public List<SeatDto> SeatDtos { get; set; }

        public HallDto(int capacity, string hallName, string movieTitle, 
            int movieDurationInMin, string movieImageUrl, DateTime seanceDate, List<SeatDto> seatDtos)
        {
            Capacity = capacity;
            HallName = hallName;
            MovieTitle = movieTitle;
            MovieDurationInMin = movieDurationInMin;
            MovieImageUrl = movieImageUrl;
            SeanceDate = seanceDate;
            SeatDtos = seatDtos;
        }
    }
}
