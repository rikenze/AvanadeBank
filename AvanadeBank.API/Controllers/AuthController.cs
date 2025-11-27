using AvanadeBank.API.Entities;
using AvanadeBank.API.Entities.Requests;
using AvanadeBank.API.Entities.Responses;
using AvanadeBank.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AvanadeBank.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ILogger<AuthController> _logger;

        private static readonly List<AppUser> Users = new()
        {
            new AppUser { Username = "admin",    Password = "admin111",    Role = "Admin" },
            new AppUser { Username = "cliente",  Password = "cliente111",  Role = "Customer" }
        };

        public AuthController(IJwtTokenService jwtTokenService, ILogger<AuthController> logger)
        {
            _jwtTokenService = jwtTokenService;
            _logger = logger;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult<LoginResponse> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var user = Users.SingleOrDefault(u =>
                u.Username == request.Username && u.Password == request.Password);

            if (user == null)
            {
                _logger.LogWarning("Tentativa de login inválida para usuário {User}", request.Username);
                return Unauthorized();
            }

            var token = _jwtTokenService.GenerateToken(user.Username, user.Role);

            _logger.LogInformation("Usuário {User} logado com sucesso com role {Role}", user.Username, user.Role);

            return new LoginResponse { Token = token };
        }
    }
}
