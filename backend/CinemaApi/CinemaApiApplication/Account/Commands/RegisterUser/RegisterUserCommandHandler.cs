using CinemaApiDomain.Entities;
using CinemaApiDomain.Interfaces;
using MediatR;

namespace CinemaApiApplication.Account.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Unit>
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);

            if (existingUser != null)
            {
                throw new InvalidOperationException("User with this email already exists.");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var newUser = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                Nationality = request.Nationality,
                PasswordHash = passwordHash
            };

            var defaultRole = new UserRole
            {
                RoleId = 3, // domyślnie widz
                User = newUser
            };

            newUser.UserRoles.Add(defaultRole);

            await _userRepository.AddAsync(newUser);

            return Unit.Value;
        }
    }
}
