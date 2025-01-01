using CinemaApiApplication.Seance;
using MediatR;

namespace CinemaApiApplication.Ticket.Queries.GetTicketsForGivenUser
{
    public class GetTicketsForGivenUserQuery : IRequest<IEnumerable<UserTicketDto>?>
    {
    }
}
