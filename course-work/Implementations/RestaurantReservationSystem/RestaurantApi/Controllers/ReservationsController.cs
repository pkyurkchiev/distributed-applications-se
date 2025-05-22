using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantApi.Data;
using RestaurantApi.Models;
using Microsoft.AspNetCore.Authorization;


namespace RestaurantApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReservationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            return await _context.Reservations
                .Include(r => r.Client)
                .Include(r => r.Table)
                .ToListAsync();
        }

        // GET: api/reservations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Client)
                .Include(r => r.Table)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
                return NotFound();

            return reservation;
        }

        // POST: api/reservations
        // POST: api/reservations
[HttpPost]
public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
{
    // 🔍 Търси клиента по име, ако ClientId е 0
    if (reservation.ClientId == 0 && reservation.Client != null && !string.IsNullOrWhiteSpace(reservation.Client.Name))
    {
        var existingClient = await _context.Clients
            .FirstOrDefaultAsync(c => c.Name.ToLower() == reservation.Client.Name.ToLower());

        if (existingClient == null)
            return BadRequest("Клиентът не съществува.");

        reservation.ClientId = existingClient.Id;
    }

    // 🔍 Търси масата по номер, ако TableId е 0
    if (reservation.TableId == 0 && reservation.Table != null)
    {
        var existingTable = await _context.Tables
            .FirstOrDefaultAsync(t => t.Number == reservation.Table.Number && t.IsAvailable);

        if (existingTable == null)
            return BadRequest("Масата не съществува или не е налична.");

        reservation.TableId = existingTable.Id;
        existingTable.IsAvailable = false;
    }
    else
    {
        // Ако все пак има TableId, го проверяваме
        var table = await _context.Tables.FindAsync(reservation.TableId);
        if (table == null || !table.IsAvailable)
            return BadRequest("Масата не съществува или не е налична.");

        table.IsAvailable = false;
    }

    _context.Reservations.Add(reservation);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetReservation), new { id = reservation.Id }, reservation);
}


        // PUT: api/reservations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, Reservation reservation)
        {
            if (id != reservation.Id)
                return BadRequest();

            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Reservations.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/reservations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
                return NotFound();

            // Освобождаване на масата
            var table = await _context.Tables.FindAsync(reservation.TableId);
            if (table != null)
                table.IsAvailable = true;

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/reservations/search?clientId=1&date=2025-05-13
        [HttpGet("search")]
public async Task<ActionResult<IEnumerable<Reservation>>> SearchReservations(string? clientName, int? tableNumber)
{
    var query = _context.Reservations
        .Include(r => r.Client)
        .Include(r => r.Table)
        .AsQueryable();

    if (!string.IsNullOrEmpty(clientName))
        query = query.Where(r => r.Client != null && r.Client.Name.ToLower().Contains(clientName.ToLower()));

    if (tableNumber.HasValue)
        query = query.Where(r => r.Table != null && r.Table.Number == tableNumber.Value);

    return await query.ToListAsync();
}


    }
}
