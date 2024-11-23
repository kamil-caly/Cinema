using MediatR;

namespace CinemaApiApplication.Account.Commands.RegisterUser
{
    public class RegisterUserCommand : RegisterUserDto, IRequest<Unit>
    {
    }
}
