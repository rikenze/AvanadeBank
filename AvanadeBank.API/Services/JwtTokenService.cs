using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace AvanadeBank.API.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly string _issuer;

        public JwtTokenService(SymmetricSecurityKey key, string issuer)
        {
            _key = key;
            _issuer = issuer;
        }

        public string GenerateToken(string username, string role)
        {
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new(ClaimTypes.Name, username),
            new(ClaimTypes.Role, role)
        };

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: null,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
