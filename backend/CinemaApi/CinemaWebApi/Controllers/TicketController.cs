﻿using CinemaApiApplication.Seance.Queries;
using CinemaApiApplication.Seance;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CinemaApiApplication.Ticket;
using CinemaApiApplication.Movie.Queries;
using CinemaApiApplication.Movie;
using CinemaApiApplication.Ticket.Queries.GetTicketsForGivenUser;
using CinemaApiApplication.Ticket.Commands.CreateTicketForGivenSeance;
using CinemaApiApplication.Ticket.Commands.UpdateTicketState;

namespace CinemaWebApi.Controllers
{
    [Route("api/ticket")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost("createTicket")]
        public async Task<ActionResult<string>> CreateTicket([FromBody] CreateTicketForGivenSeanceCommand command)
        {
            string reservationCode = await _mediator.Send(command);
            return Ok(reservationCode);
        }

        [Authorize]
        [HttpGet("getTickets")]
        public async Task<ActionResult<IEnumerable<UserTicketDto>>> GetTickets([FromQuery] bool userRequest)
        {
            var ticketDtos = await _mediator.Send(new GetAllTicketsOrForGivenUserQuery(userRequest));
            return Ok(ticketDtos);
        }

        [Authorize(Roles = "Admin,Ticketer")]
        [HttpPut("changeState")]
        public async Task<ActionResult> ChangeTicketState([FromQuery] string reservationCode)
        {
            bool isUpdated = await _mediator.Send(new UpdateTicketStateCommand() { ReservationCode = reservationCode });
            return Ok(isUpdated);
        }
    }
}
