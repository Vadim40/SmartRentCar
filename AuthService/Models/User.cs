using System.ComponentModel.DataAnnotations;

namespace AuthService.Models
{
    public class User
    {
        public int Id { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public List<UserRole> UserRoles { get; set; }
    }
}
