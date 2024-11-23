using CinemaApiApplication.Movie.Queries;
using CinemaApiApplication.Movie;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CinemaApiApplication.Account.Commands.RegisterUser;
using FluentValidation;

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
    }
}
