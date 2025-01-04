using CinemaApiDomain.Entities.Enums;
using CinemaApiDomain.Interfaces;
using MediatR;

namespace CinemaApiApplication.Ticket.Commands.UpdateTicketState
{
    public class UpdateTicketStateCommandHandler : IRequestHandler<UpdateTicketStateCommand, bool>
    {
        private readonly ITicketRepository _ticketRepository;
        public UpdateTicketStateCommandHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
        public async Task<bool> Handle(UpdateTicketStateCommand request, CancellationToken cancellationToken)
        {
            var ticket = await _ticketRepository.GetTicketAsync(request.ReservationCode);

            if (ticket == null)
            {
                throw new Exception("Ticket for given reservation code not found");
            }

            if (ticket.Status != TicketState.Valid)
            {
                return false;
            }

            if (ticket.Seance.Date >= DateTime.Now.AddMinutes(15))
            {
                ticket.Status = TicketState.Used;
            }
            else
            {
                ticket.Status = TicketState.Invalid;
            }

            await _ticketRepository.UpdateStateAsync(ticket);
            return true;
        }
    }
}
