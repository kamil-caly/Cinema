using CinemaApiApplication.Hall;
using CinemaApiApplication.Hall.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWebApi.Controllers
{
    [Route("api/hall")]
    [ApiController]
    public class HallController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HallController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Viewer")]
        [HttpGet("getHallForGivenSeance")]
        public async Task<ActionResult<HallDto?>> GetHallForGivenSeance([FromQuery] DateTime dateTime)
        {
            var hallDto = await _mediator.Send(new GetHallForGivenSeanceDateQuery(dateTime));

            return Ok(hallDto);
        }
    }
}
