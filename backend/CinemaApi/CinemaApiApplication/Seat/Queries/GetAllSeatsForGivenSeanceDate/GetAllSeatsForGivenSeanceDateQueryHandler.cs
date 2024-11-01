using CinemaApiApplication.Seance.Queries;
using CinemaApiApplication.Seance;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaApiDomain.Interfaces;

namespace CinemaApiApplication.Seat.Queries.GetAllSeatsForGivenSeanceDate
{
    public class GetAllSeatsForGivenSeanceDateQueryHandler : IRequestHandler<GetAllSeatsForGivenSeanceDateQuery, IEnumerable<SeatDto>?>
    {
        private readonly ISeatRepository _seatRepository;

        public GetAllSeatsForGivenSeanceDateQueryHandler(ISeatRepository seatRepository)
        {
            this._seatRepository = seatRepository;
        }
        public async Task<IEnumerable<SeatDto>?> Handle(GetAllSeatsForGivenSeanceDateQuery request, CancellationToken cancellationToken)
        {
            var seats = await _seatRepository.GetAllForGivenSeance(request._dateTime);

            if (seats != null)
            {
                return seats.Select(s => new SeatDto(s)).ToList();
            }

            return null;
        }
    }
}
