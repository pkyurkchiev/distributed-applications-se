using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using expensetracker.Data;
using expensetracker.Models;
using System.Security.Claims;

namespace expensetracker.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetProfile()
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (!int.TryParse(userIdClaim, out var userId))
                return Unauthorized(new { message = "Invalid token or userId claim." });

            var user = await _context.Users.FindAsync(userId);
            if (user == null) return NotFound();

            return Ok(new
            {
                user.Id,
                user.Email,
                user.FullName,
                user.CreatedAt
            });
        }

        [HttpPut("me")]
        public async Task<IActionResult> UpdateProfile([FromBody] User updated)
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value!);
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return NotFound();

            user.FullName = updated.FullName;
            user.Email = updated.Email;
            await _context.SaveChangesAsync();

            return Ok(user);
        }
        [HttpDelete("me")]
        public async Task<IActionResult> DeleteProfile()
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value!);

            // Взимаме потребителя
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return NotFound();

            // Взимаме акаунтите му
            var accounts = await _context.Accounts
                .Where(a => a.UserId == userId)
                .ToListAsync();

            foreach (var account in accounts)
            {
                // Взимаме и изтриваме всички транзакции по този акаунт
                var transactions = await _context.Transactions
                    .Where(t => t.AccountId == account.Id)
                    .ToListAsync();

                _context.Transactions.RemoveRange(transactions);
            }

            // Изтриваме акаунтите
            _context.Accounts.RemoveRange(accounts);

            // Накрая — изтриваме самия потребител
            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return Ok("Профилът и всички данни са изтрити.");
        }

    }
}
