using CinemaApiApplication.Seance;
using MediatR;

namespace CinemaApiApplication.Ticket.Queries.GetTicketsForGivenUser
{
    public class GetAllTicketsOrForGivenUserQuery : IRequest<IEnumerable<UserTicketDto>?>
    {
        public bool _userRequest { get; set; }

        public GetAllTicketsOrForGivenUserQuery(bool userRequest)
        {
            _userRequest = userRequest;
        }
    }
}
