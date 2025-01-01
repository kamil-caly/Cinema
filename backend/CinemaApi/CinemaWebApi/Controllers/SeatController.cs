using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWebApi.Controllers
{
    [Route("api/seat")]
    [ApiController]
    public class SeatController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SeatController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
