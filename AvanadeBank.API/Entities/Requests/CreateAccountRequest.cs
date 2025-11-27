using System.ComponentModel.DataAnnotations;

namespace AvanadeBank.API.Entities.Requests
{
    public class CreateAccountRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string OwnerName { get; set; } = default!;

        [Range(0, double.MaxValue)]
        public decimal InitialBalance { get; set; }
    }
}
