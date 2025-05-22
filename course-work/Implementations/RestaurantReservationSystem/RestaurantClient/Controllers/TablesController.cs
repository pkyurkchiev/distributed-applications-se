using Microsoft.AspNetCore.Mvc;
using RestaurantClient.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace RestaurantClient.Controllers
{
    public class TablesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TablesController(IHttpClientFactory httpClientFactory)
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

        public async Task<IActionResult> Index(int? number, int? capacity)

        { 
    var client = CreateClient();

    string url = "Tables";
    if (number.HasValue || capacity.HasValue)
    {
        var query = new List<string>();
        if (number.HasValue) query.Add($"number={number.Value}");
        if (capacity.HasValue) query.Add($"capacity={capacity.Value}");
        url = "Tables/search?" + string.Join("&", query);
    }

    var response = await client.GetAsync(url);
    if (!response.IsSuccessStatusCode) return View(new List<Table>());

    var json = await response.Content.ReadAsStringAsync();
    var tables = JsonSerializer.Deserialize<List<Table>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    return View(tables);
    }


        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Table model)
        {
            if (!ModelState.IsValid) return View(model);

            var client = CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("Tables", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = CreateClient();
            var response = await client.GetAsync($"Tables/{id}");
            if (!response.IsSuccessStatusCode) return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var model = JsonSerializer.Deserialize<Table>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Table model)
        {
            if (!ModelState.IsValid) return View(model);

            var client = CreateClient();
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"Tables/{model.Id}", content);

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index");

            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = CreateClient();
            var response = await client.DeleteAsync($"Tables/{id}");

            return RedirectToAction("Index");
        }
    }
}
