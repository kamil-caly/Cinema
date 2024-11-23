using CinemaApiDomain.Entities;

namespace CinemaApiApplication.Account
{
    public class RegisterUserDto
    {
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime? DateOfBirth { get; set; }
        public string? Nationality { get; set; }
    }
}
