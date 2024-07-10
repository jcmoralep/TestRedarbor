using System.ComponentModel.DataAnnotations;

namespace Inventory.Application.DTOs
{
    public class UserRegistrationRequestDto
    {
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
