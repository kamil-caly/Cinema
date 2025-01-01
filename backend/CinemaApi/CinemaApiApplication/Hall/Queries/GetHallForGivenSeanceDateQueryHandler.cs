using CinemaApiApplication.Seat;
using CinemaApiDomain.Interfaces;
using MediatR;

namespace CinemaApiApplication.Hall.Queries
{
    public class GetHallForGivenSeanceDateQueryHandler : IRequestHandler<GetHallForGivenSeanceDateQuery, HallDto?>
    {
        private readonly IHallRepository _hallRepository;

        public GetHallForGivenSeanceDateQueryHandler(IHallRepository hallRepository)
        {
            this._hallRepository = hallRepository;
        }
        public async Task<HallDto?> Handle(GetHallForGivenSeanceDateQuery request, CancellationToken cancellationToken)
        {
            var hall = await _hallRepository.GetHallForGivenSeance(request._dateTime);

            if (hall != null)
            {
                List<SeatDto> seatDtos = new List<SeatDto>();
                var seance = hall.Seances.FirstOrDefault();

                if (seance == null)
                {
                    return null;
                }

                foreach (var ticket in seance.Tickets)
                {
                    foreach (var seat in ticket.Seats)
                    {
                        seatDtos.Add(new SeatDto(seat));
                    }
                }

                return new HallDto(hall.Capacity, hall.Name, hall.Seances.First().Movie.Title,
                     hall.Seances.First().Movie.DurationInMin, hall.Seances.First().Movie.ImageUrl,
                     seance.Date, seatDtos);
            }

            return null;
        }
    }
}
