using CinemaApiDomain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CinemaApiApplication.Ticket.Queries.GetTicketsForGivenUser
{
    public class GetTicketsForGivenUserQueryHandler : IRequestHandler<GetTicketsForGivenUserQuery, IEnumerable<UserTicketDto>?>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetTicketsForGivenUserQueryHandler(ITicketRepository ticketRepository, IHttpContextAccessor httpContextAccessor)
        {
            _ticketRepository = ticketRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IEnumerable<UserTicketDto>?> Handle(GetTicketsForGivenUserQuery request, CancellationToken cancellationToken)
        {
            string? email = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;

            if (email == null)
            {
                throw new Exception("User email is null");
            }

            var tickets = await _ticketRepository.GetTicketsForUserAsync(email);

            return tickets.Select(t => new UserTicketDto
            {
                ReservationCode = t.ReservationCode,
                Status = t.Status,
                PurchaseDate = t.PurchaseDate,
                UserEmail = t.UserEmail,
                HallName = t.Seance.Hall.Name,
                MovieTitle = t.Seance.Movie.Title,
                SeanceDate = t.Seance.Date,
                SeatDtos = t.Seats.Select(s => new Seat.SeatDto()
                {
                    Row = s.Row,
                    Num = s.Number,
                    VIP = s.VIP
                }).ToList()
            }).OrderBy(t => t.SeanceDate);
        }
    }
}
