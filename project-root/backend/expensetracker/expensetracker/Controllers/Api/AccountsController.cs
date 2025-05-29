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
    public class AccountsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AccountsController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value!);
            var accounts = await _context.Accounts
                .Where(a => a.UserId == userId)
                .ToListAsync();

            return Ok(accounts);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount(int id)
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value!);
            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);

            if (account == null)
                return NotFound();

            return Ok(account);
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] Account model)
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value!);
            model.UserId = userId;
            model.CreatedAt = DateTime.UtcNow;
            model.Balance = 0; 

            _context.Accounts.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAccount), new { id = model.Id }, model);
        }

    
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody] Account updated)
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value!);
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
            if (account == null)
                return NotFound();

            account.Name = updated.Name;
            account.Type = updated.Type;
            account.Balance = updated.Balance;

            await _context.SaveChangesAsync();
            return Ok(account);
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var userId = int.Parse(User.FindFirst("userId")?.Value!);
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
            if (account == null)
                return NotFound();

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
