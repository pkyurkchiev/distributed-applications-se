using MC.Infrastructure.Messaging.Responses.Authentications;
using MC.Infrastructure.Messaging.Responses.Movies;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MC.Website.Controllers
{
    public class MoviesController : Controller
    {
        private readonly Uri uri = new("http://localhost:5004/api/movies");

        public async Task<IActionResult> Index()
        {
            var token = await GetAccessToken();

            using (HttpClient client = new())
            {
                client.BaseAddress = uri;

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                HttpResponseMessage response = await client.GetAsync("");
                var jsonContent = await response.Content.ReadAsStringAsync();
                var responseData = JsonSerializer.Deserialize<GetMoviesResponse>(jsonContent, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            return View();
        }

        private static async Task<string> GetAccessToken()
        {
            using (HttpClient client = new())
            {
                client.BaseAddress = new("http://localhost:5004/api/authorization?clientId=fmi&secret=fmi");

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                HttpResponseMessage response = await client.GetAsync("");
                var jsonContent = await response.Content.ReadAsStringAsync();
                var responseData = JsonSerializer.Deserialize<AuthenticationResponse>(jsonContent, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                return responseData.Token;
            }
        }
    }
}
