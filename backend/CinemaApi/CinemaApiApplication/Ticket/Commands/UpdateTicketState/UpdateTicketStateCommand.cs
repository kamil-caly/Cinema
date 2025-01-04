using MediatR;

namespace CinemaApiApplication.Ticket.Commands.UpdateTicketState
{
    public class UpdateTicketStateCommand: UpdateTicketStateDto, IRequest<bool>
    {
    }
}
