namespace AvanadeBank.API.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(string username, string role);
    }
}
