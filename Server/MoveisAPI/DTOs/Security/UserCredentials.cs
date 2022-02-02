using System.ComponentModel.DataAnnotations;

namespace MoveisAPI.DTOs.Security
{
    public class UserCredentials
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
