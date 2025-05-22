using Microsoft.AspNetCore.Mvc;
using RestaurantClient.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;


namespace RestaurantClient.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("http://localhost:5031/");

            var response = await client.PostAsJsonAsync("api/Auth/login", model);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<JwtResponse>();
                if (result != null)
                {
                    HttpContext.Session.SetString("JWT", result.Token); // ⬅ тук
                    return RedirectToAction("Index", "Clients");
                }
            }

            ModelState.AddModelError("", "Невалидни данни за вход");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JWT"); // Премахва токена от сесията
            return RedirectToAction("Login", "Auth");
        }



        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        
        public async Task<IActionResult> Register(RegisterViewModel model)
{
    if (!ModelState.IsValid)
        return View(model);

    var request = new
    {
        Username = model.Username,
        Password = model.Password,
        FullName = model.FullName,
        Email = model.Email,
        Age = model.Age
    };

    // Изпращане към API
    var client = _httpClientFactory.CreateClient();
    client.BaseAddress = new Uri("http://localhost:5031/api/");

    var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
    var response = await client.PostAsync("Auth/register", content);

    if (response.IsSuccessStatusCode)
        return RedirectToAction("Login");

    ModelState.AddModelError("", "Грешка при регистрация.");
    return View(model);
}

        
    }
}
