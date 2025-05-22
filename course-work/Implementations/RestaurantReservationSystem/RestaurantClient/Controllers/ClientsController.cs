using Microsoft.AspNetCore.Mvc;
using RestaurantClient.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace RestaurantClient.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ClientsController(IHttpClientFactory httpClientFactory)
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

        // GET: Clients
        public async Task<IActionResult> Index(string? name, string? phone)
{
    var client = CreateClient();

    string url = "Clients";
    if (!string.IsNullOrEmpty(name) || !string.IsNullOrEmpty(phone))
    {
        var query = new List<string>();
        if (!string.IsNullOrEmpty(name))
            query.Add($"name={Uri.EscapeDataString(name)}");
        if (!string.IsNullOrEmpty(phone))
            query.Add($"phone={Uri.EscapeDataString(phone)}");

        url = $"Clients/search?{string.Join("&", query)}";
    }

    var response = await client.GetAsync(url);
    if (!response.IsSuccessStatusCode)
        return View(new List<Client>());

    var json = await response.Content.ReadAsStringAsync();
    var clients = JsonSerializer.Deserialize<List<Client>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    return View(clients);
}


        // GET: Clients/Create
        public IActionResult Create() => View();

        // POST: Clients/Create
        [HttpPost]
        public async Task<IActionResult> Create(Client model)
        {
            if (!ModelState.IsValid) return View(model);

            var client = CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Clients", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(model);
        }

        // GET: Clients/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var client = CreateClient();
            var response = await client.GetAsync($"Clients/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var model = JsonSerializer.Deserialize<Client>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(model);
        }

        // POST: Clients/Edit/1
        [HttpPost]
        public async Task<IActionResult> Edit(Client model)
        {
            if (!ModelState.IsValid) return View(model);

            var client = CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"Clients/{model.Id}", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(model);
        }

        // GET: Clients/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var client = CreateClient();
            var response = await client.DeleteAsync($"Clients/{id}");

            return RedirectToAction("Index");
        }
    }
}
