namespace AvanadeBank.API.Entities
{
    public class AppUser
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string Role { get; set; } = default!;
    }
}
