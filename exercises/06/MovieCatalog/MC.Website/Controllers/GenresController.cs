using MC.Website.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MC.Website.Controllers
{
    public class GenresController : Controller
    {
        private readonly Uri url = new Uri("http://localhost:49272/api/genres/");

        private static async Task<string> GetAccessToken()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:49272");

                // We want the response to be JSON.
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Build up the data to POST.
                List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", "pavel"),
                    new KeyValuePair<string, string>("password", "123123")
                };

                FormUrlEncodedContent content = new FormUrlEncodedContent(postData);

                // Post to the Server and parse the response.
                HttpResponseMessage response = await client.PostAsync("Token", content);
                string jsonString = await response.Content.ReadAsStringAsync();
                object responseData = JsonConvert.DeserializeObject(jsonString);

                // return the Access Token.
                return ((dynamic)responseData).access_token;
            }
        }

        // GET: Genres
        public async Task<ActionResult> Index()
        {
            string accessToken = await GetAccessToken();

            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Add the Authorization header with the AccessToken.
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                // make the request
                HttpResponseMessage response = await client.GetAsync("getall");

                // parse the response and return the data.
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<List<GenreVM>>(jsonString);
                return View(responseData);
            }
        }

        // GET: Genres/Details/5
        public async Task<ActionResult> Details(int id)
        {
            string accessToken = await GetAccessToken();

            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Add the Authorization header with the AccessToken.
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                // make the request
                HttpResponseMessage response = await client.GetAsync("getbyid/" + id);

                // parse the response and return the data.
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<GenreVM>(jsonString);
                return View(responseData);
            }
        }

        // GET: Genres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        [HttpPost]
        public async Task<ActionResult> Create(GenreVM genreVM)
        {
            try
            {
                string accessToken = await GetAccessToken();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Add the Authorization header with the AccessToken.
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                    var content = JsonConvert.SerializeObject(genreVM);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    // make the request
                    HttpResponseMessage response = await client.PostAsync("save", byteContent);

                    // parse the response and return the data.
                    //string jsonString = await response.Content.ReadAsStringAsync();
                    //var responseData = JsonConvert.DeserializeObject<GenreVM>(jsonString);
                    //return View(responseData);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Genres/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            string accessToken = await GetAccessToken();

            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Add the Authorization header with the AccessToken.
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                // make the request
                HttpResponseMessage response = await client.GetAsync("getbyid/" + id);

                // parse the response and return the data.
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<GenreVM>(jsonString);
                return View(responseData);
            }
        }

        // POST: Genres/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(GenreVM genreVM)
        {
            try
            {
                string accessToken = await GetAccessToken();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Add the Authorization header with the AccessToken.
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                    var content = JsonConvert.SerializeObject(genreVM);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    // make the request
                    HttpResponseMessage response = await client.PostAsync("save", byteContent);

                    // parse the response and return the data.
                    //string jsonString = await response.Content.ReadAsStringAsync();
                    //var responseData = JsonConvert.DeserializeObject<GenreVM>(jsonString);
                    //return View(responseData);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Genres/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            string accessToken = await GetAccessToken();

            using (var client = new HttpClient())
            {
                client.BaseAddress = url;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Add the Authorization header with the AccessToken.
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                // make the request
                HttpResponseMessage response = await client.GetAsync("getbyid/" + id);

                // parse the response and return the data.
                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<GenreVM>(jsonString);
                return View(responseData);
            }
        }

        // POST: Genres/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                string accessToken = await GetAccessToken();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = url;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Add the Authorization header with the AccessToken.
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                    // make the request
                    HttpResponseMessage response = await client.DeleteAsync("delete/" + id);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
