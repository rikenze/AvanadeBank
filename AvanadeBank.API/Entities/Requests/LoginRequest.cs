using System.ComponentModel.DataAnnotations;

namespace AvanadeBank.API.Entities.Requests
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;
    }
}
