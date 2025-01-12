using CinemaApiApplication.Account;
using CinemaApiApplication.Account.Commands.LoginUser;
using CinemaApiApplication.Account.Commands.RegisterUser;
using CinemaApiApplication.Account.Queries.GetUserData;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CinemaWebApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<RegisterUserCommand> _validator;

        public AccountController(IMediator mediator, IValidator<RegisterUserCommand> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        [HttpPost("register")]
        public async Task<IResult> Register([FromBody] RegisterUserCommand command)
        {
            var validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            await _mediator.Send(command);

            return Results.Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginUserCommand command)
        {
            string token = await _mediator.Send(command);
            return Ok(token);
        }

        [Authorize]
        [HttpGet("getUserData")]
        public async Task<ActionResult<UserDataDto>> GetUserData([FromQuery] string token)
        {
            UserDataDto userData = await _mediator.Send(new GetUserDataQuery(token));
            return Ok(userData);
        }
    }
}
