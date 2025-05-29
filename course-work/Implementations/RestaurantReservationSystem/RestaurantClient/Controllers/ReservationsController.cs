using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantClient.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace RestaurantClient.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ReservationsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private HttpClient CreateClient()
        {
            var client = _httpClientFactory.CreateClient();
            var token = HttpContext.Session.GetString("JWT");
            client.BaseAddress = new Uri("http://localhost:5031/api/");
            if (!string.IsNullOrEmpty(token))
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return client;
        }

        public async Task<IActionResult> Index(string? clientName, int? tableNumber)
{
    var client = CreateClient();
    string url = "Reservations";

    if (!string.IsNullOrEmpty(clientName) || tableNumber.HasValue)
    {
        var queryParams = new List<string>();
        if (!string.IsNullOrEmpty(clientName)) queryParams.Add($"clientName={Uri.EscapeDataString(clientName)}");
        if (tableNumber.HasValue) queryParams.Add($"tableNumber={tableNumber.Value}");
        url = "Reservations/search?" + string.Join("&", queryParams);
    }

    var response = await client.GetAsync(url);
    if (!response.IsSuccessStatusCode) return View(new List<Reservation>());

    var json = await response.Content.ReadAsStringAsync();
    var reservations = JsonSerializer.Deserialize<List<Reservation>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    return View(reservations);
}


        // GET: Create
        public async Task<IActionResult> Create()
{
    var model = new ReservationFormViewModel
    {
        ReservationDate = DateTime.Now  // ➕ Текуща дата и час
    };

    await PopulateClientsAndTables(model);
    return View(model);
}


        // POST: Create
        [HttpPost]
        public async Task<IActionResult> Create(ReservationFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateClientsAndTables(model);
                return View(model);
            }

            var client = CreateClient();

            var reservation = new Reservation
            {
                ClientId = model.ClientId,
                TableId = model.TableId,
                ReservationDate = model.ReservationDate,
                PeopleCount = model.PeopleCount,
                Notes = model.Notes
            };

            var content = new StringContent(JsonSerializer.Serialize(reservation), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Reservations", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "Грешка при създаване на резервация.");
            await PopulateClientsAndTables(model);
            return View(model);
        }

        private async Task PopulateClientsAndTables(ReservationFormViewModel model)
        {
            var client = CreateClient();

            // Клиенти
            var clientsResponse = await client.GetAsync("Clients");
            if (clientsResponse.IsSuccessStatusCode)
            {
                var clientsJson = await clientsResponse.Content.ReadAsStringAsync();
                var clients = JsonSerializer.Deserialize<List<Client>>(clientsJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                model.Clients = clients.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();
            }

            // Маси
            var tablesResponse = await client.GetAsync("Tables");
            if (tablesResponse.IsSuccessStatusCode)
            {
                var tablesJson = await tablesResponse.Content.ReadAsStringAsync();
                var tables = JsonSerializer.Deserialize<List<Table>>(tablesJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                model.Tables = tables.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = $"Маса №{t.Number}"
                }).ToList();
            }
        }

        public async Task<IActionResult> Edit(int id)
{
    var client = CreateClient();
    var response = await client.GetAsync($"Reservations/{id}");
    if (!response.IsSuccessStatusCode) return NotFound();

    var json = await response.Content.ReadAsStringAsync();
    var reservation = JsonSerializer.Deserialize<Reservation>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    var model = new ReservationFormViewModel
    {
        Id = reservation.Id,
        ClientId = reservation.ClientId,
        TableId = reservation.TableId,
        ReservationDate = reservation.ReservationDate,
        PeopleCount = reservation.PeopleCount,
        Notes = reservation.Notes
    };

    await PopulateClientsAndTables(model);
    return View(model);
}

[HttpPost]
public async Task<IActionResult> Edit(ReservationFormViewModel model)
{
    if (!ModelState.IsValid)
    {
        await PopulateClientsAndTables(model);
        return View(model);
    }

    var client = CreateClient();
    var reservation = new Reservation
    {
        Id = model.Id,
        ClientId = model.ClientId,
        TableId = model.TableId,
        ReservationDate = model.ReservationDate,
        PeopleCount = model.PeopleCount,
        Notes = model.Notes
    };

    var content = new StringContent(JsonSerializer.Serialize(reservation), Encoding.UTF8, "application/json");
    var response = await client.PutAsync($"Reservations/{model.Id}", content);

    if (response.IsSuccessStatusCode)
        return RedirectToAction("Index");

    ModelState.AddModelError("", "Неуспешна редакция на резервация.");
    await PopulateClientsAndTables(model);
    return View(model);
}


        
        public async Task<IActionResult> Delete(int id)
        {
            var client = CreateClient();
            await client.DeleteAsync($"Reservations/{id}");
            return RedirectToAction("Index");
        }
    }
}
