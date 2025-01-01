using MediatR;

namespace CinemaApiApplication.Ticket.Commands
{
    public class CreateTicketForGivenSeanceCommand: CreateTicketDto, IRequest<string>
    {
    }
}
