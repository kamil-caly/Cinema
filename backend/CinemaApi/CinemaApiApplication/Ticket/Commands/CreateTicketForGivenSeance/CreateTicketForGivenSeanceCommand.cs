using MediatR;

namespace CinemaApiApplication.Ticket.Commands.CreateTicketForGivenSeance
{
    public class CreateTicketForGivenSeanceCommand : CreateTicketDto, IRequest<string>
    {
    }
}
