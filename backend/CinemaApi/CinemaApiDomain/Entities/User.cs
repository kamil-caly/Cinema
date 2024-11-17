using System.Data;

namespace CinemaApiDomain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime? DateOfBirth { get; set; }
        public string? Nationality { get; set; }
        public string PasswordHash { get; set; } = default!;
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
