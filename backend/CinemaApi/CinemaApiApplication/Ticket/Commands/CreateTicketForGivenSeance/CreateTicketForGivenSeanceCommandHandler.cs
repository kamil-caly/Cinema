using CinemaApiDomain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CinemaApiApplication.Ticket.Commands.CreateTicketForGivenSeance
{
    public class CreateTicketForGivenSeanceCommandHandler : IRequestHandler<CreateTicketForGivenSeanceCommand, string>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ISeanceRepository _seanceRepository;
        private readonly ISeatRepository _seatRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateTicketForGivenSeanceCommandHandler(ITicketRepository ticketRepository, IHttpContextAccessor httpContextAccessor,
            ISeanceRepository seanceRepository, ISeatRepository seatRepository)
        {
            _ticketRepository = ticketRepository;
            _httpContextAccessor = httpContextAccessor;
            _seanceRepository = seanceRepository;
            _seatRepository = seatRepository;
        }
        public async Task<string> Handle(CreateTicketForGivenSeanceCommand request, CancellationToken cancellationToken)
        {
            string? email = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            if (email == null)
            {
                throw new Exception("Email is null");
            }

            var seance = await _seanceRepository.GetSeanceForGivenDate(request.SeanceDate);

            if (seance == null)
            {
                throw new Exception("Seance for given date is null");
            }

            var newSeats = request.SeatDtos
                .Select(seat => new CinemaApiDomain.Entities.Seat
                {
                    Row = seat.Row,
                    Number = seat.Num,
                    VIP = seat.VIP
                })
                .ToList();

            var existingSeats = await _seatRepository.GetSeatsForGivenSeanceDate(request.SeanceDate);

            if (newSeats.Any(newSeat => existingSeats.Any(s => s.Row == newSeat.Row && s.Number == newSeat.Number)))
            {
                throw new Exception("Chosen seats are taken");
            }

            var ticket = new CinemaApiDomain.Entities.Ticket()
            {
                PurchaseDate = DateTime.Now,
                Seance = seance,
                ReservationCode = Guid.NewGuid().ToString(),
                Status = CinemaApiDomain.Entities.Enums.TicketState.Valid,
                UserEmail = email,
                Seats = newSeats
            };

            await _ticketRepository.AddAsync(ticket);

            return ticket.ReservationCode;
        }
    }
}
