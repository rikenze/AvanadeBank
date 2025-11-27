using AvanadeBank.API.Data;
using AvanadeBank.API.Entities;
using AvanadeBank.API.Entities.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AvanadeBank.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(AppDbContext db, ILogger<AccountsController> logger)
        {
            _db = db;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<BankAccount>>> GetAll(CancellationToken ct)
        {
            _logger.LogInformation("Getting all accounts (Admin).");

            var accounts = await _db.Accounts.AsNoTracking().ToListAsync(ct);
            return Ok(accounts);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<ActionResult<BankAccount>> GetById(int id, CancellationToken ct)
        {
            _logger.LogInformation("Getting account by {Id}", id);

            var account = await _db.Accounts.FindAsync([id], ct);
            if (account == null)
            {
                _logger.LogWarning("Account {Id} not found", id);
                return NotFound();
            }

            return Ok(account);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BankAccount>> Create(
            [FromBody] CreateAccountRequest request,
            CancellationToken ct)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            _logger.LogInformation("Creating new account for {Owner}", request.OwnerName);

            var account = new BankAccount
            {
                OwnerName = request.OwnerName,
                Balance = request.InitialBalance
            };

            _db.Accounts.Add(account);
            await _db.SaveChangesAsync(ct);

            return CreatedAtAction(nameof(GetById), new { id = account.Id }, account);
        }
    }
}
