using expensetracker.Data;
using expensetracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TransactionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TransactionsController(AppDbContext context)
    {
        _context = context;
    }

  
    [HttpGet]
    public async Task<IActionResult> GetTransactions(
        [FromQuery] string? type,
        [FromQuery] string? category,
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string sortBy = "date_desc"
    )
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);

        var query = _context.Transactions
            .Include(t => t.Account)
            .Where(t => t.Account.UserId == userId);

        if (!string.IsNullOrEmpty(type))
            query = query.Where(t => t.Type == type);

        if (!string.IsNullOrEmpty(category))
            query = query.Where(t => t.Category.Contains(category));

        if (from.HasValue)
            query = query.Where(t => t.Date >= from.Value);

        if (to.HasValue)
            query = query.Where(t => t.Date <= to.Value);

        query = sortBy switch
        {
            "amount_asc" => query.OrderBy(t => t.Amount),
            "amount_desc" => query.OrderByDescending(t => t.Amount),
            "date_asc" => query.OrderBy(t => t.Date),
            _ => query.OrderByDescending(t => t.Date)
        };

        var total = await query.CountAsync();
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(t => new
            {
                t.Id,
                t.Type,
                t.Amount,
                t.Category,
                t.Date,
                t.Note,
                Account = new
                {
                    t.Account.Id,
                    t.Account.Name,
                    t.Account.Type
                }
            })
            .ToListAsync();

        return Ok(new
        {
            total,
            page,
            pageSize,
            transactions = items
        });
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetTransaction(int id)
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);

        var transaction = await _context.Transactions
            .Include(t => t.Account)
            .FirstOrDefaultAsync(t => t.Id == id && t.Account.UserId == userId);

        if (transaction == null)
            return NotFound();

        return Ok(new
        {
            transaction.Id,
            transaction.Type,
            transaction.Amount,
            transaction.Category,
            transaction.Date,
            transaction.Note,
            Account = new
            {
                transaction.Account.Id,
                transaction.Account.Name,
                transaction.Account.Type
            }
        });
    }

   
 
    [HttpPost]
    public async Task<IActionResult> CreateTransaction([FromBody] Transaction model)
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);

        var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == model.AccountId && a.UserId == userId);
        if (account == null)
            return BadRequest("Invalid account.");

        _context.Transactions.Add(model);

        if (model.Type == "income")
            account.Balance += model.Amount;
        else if (model.Type == "expense")
            account.Balance -= model.Amount;

        await _context.SaveChangesAsync();

        
        var transactionWithAccount = await _context.Transactions
            .Include(t => t.Account)
            .Where(t => t.Id == model.Id)
            .Select(t => new
            {
                t.Id,
                t.Type,
                t.Amount,
                t.Category,
                t.Date,
                t.Note,
                Account = new
                {
                    t.Account.Id,
                    t.Account.Name,
                    t.Account.Type
                }
            })
            .FirstOrDefaultAsync();

        return CreatedAtAction(nameof(GetTransaction), new { id = model.Id }, transactionWithAccount);
    }


    // PUT: /api/transactions/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTransaction(int id, [FromBody] Transaction updated)
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);
        var transaction = await _context.Transactions
            .Include(t => t.Account)
            .FirstOrDefaultAsync(t => t.Id == id && t.Account.UserId == userId);

        if (transaction == null) return NotFound();

        
        if (transaction.Type == "income")
            transaction.Account.Balance -= transaction.Amount;
        else if (transaction.Type == "expense")
            transaction.Account.Balance += transaction.Amount;

        // 2️⃣ Update fields
        transaction.Type = updated.Type;
        transaction.Amount = updated.Amount;
        transaction.Category = updated.Category;
        transaction.Note = updated.Note;
        transaction.Date = updated.Date;

        // 3️⃣ Apply the new transaction’s impact
        if (transaction.Type == "income")
            transaction.Account.Balance += transaction.Amount;
        else if (transaction.Type == "expense")
            transaction.Account.Balance -= transaction.Amount;

        await _context.SaveChangesAsync();
        return Ok(transaction);
    }

    // DELETE: /api/transactions/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTransaction(int id)
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);
        var transaction = await _context.Transactions
            .Include(t => t.Account)
            .FirstOrDefaultAsync(t => t.Id == id && t.Account.UserId == userId);

        if (transaction == null) return NotFound();

        // Revert its impact
        if (transaction.Type == "income")
            transaction.Account.Balance -= transaction.Amount;
        else if (transaction.Type == "expense")
            transaction.Account.Balance += transaction.Amount;

        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();
        return NoContent();
    }


    // ✅ Обобщена статистика на транзакции
    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary([FromQuery] DateTime? from, [FromQuery] DateTime? to)
    {
        var userId = int.Parse(User.FindFirst("userId")!.Value);

        var query = _context.Transactions
            .Include(t => t.Account)
            .Where(t => t.Account.UserId == userId);

        if (from.HasValue)
            query = query.Where(t => t.Date >= from.Value);

        if (to.HasValue)
            query = query.Where(t => t.Date <= to.Value);

        var transactions = await query.ToListAsync();

        var totalIncome = transactions.Where(t => t.Type == "income").Sum(t => t.Amount);
        var totalExpense = transactions.Where(t => t.Type == "expense").Sum(t => t.Amount);

        return Ok(new
        {
            from = from?.ToString("yyyy-MM-dd"),
            to = to?.ToString("yyyy-MM-dd"),
            totalIncome,
            totalExpense,
            netBalance = totalIncome - totalExpense
        });
    }
}
