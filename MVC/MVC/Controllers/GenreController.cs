using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;

namespace MVC.Controllers
{
    public class GenreController : Controller
    {
        public const string baseUrl = "http://localhost:27169/";
        private readonly Uri _clientBaseAddress = new Uri(baseUrl);
        private readonly HttpClient _client;

        public GenreController()
        {
            _client = new HttpClient();
            _client.BaseAddress = _clientBaseAddress;
        }
        private void HeaderClearing()
        {
            // Clearing default headers
            _client.DefaultRequestHeaders.Clear();

            // Define the request type of the data
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        // GET: Genre
        public async Task<ActionResult> Index()
        {
            List<Genre> genres = new List<Genre>();
            HeaderClearing();

            HttpResponseMessage httpResponseMessage = await _client.GetAsync("api/Genre");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                genres = JsonConvert.DeserializeObject<List<Genre>>(responseMessage);
            }
            return View(genres);
        }

        // GET: Genre/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Genre genre = new Genre();
            HeaderClearing();

            HttpResponseMessage httpResponseMessage = await _client.GetAsync($"api/Genre/{id}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                genre = JsonConvert.DeserializeObject<Genre>(responseMessage);
            }
            return View(genre);
        }

        // GET: Genre/Create
        public async Task<ActionResult> CreateAsync()
        {
            Genre genre = new Genre();
            HeaderClearing();
            return View(genre);
        }

        // POST: Genre/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                string createGenreInfo = JsonConvert.SerializeObject(genre);
                StringContent stringContentInfo = new StringContent(createGenreInfo, Encoding.UTF8, "application/json");
                HttpResponseMessage createHttpResponseMessage = _client.PostAsync(_client.BaseAddress + "api/Genre", stringContentInfo).Result;
                if (createHttpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(genre);
        }

        // GET: Genre/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Genre genre = new Genre();
            HeaderClearing();

            HttpResponseMessage httpResponseMessage = await _client.GetAsync($"api/Genre/{id}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                genre = JsonConvert.DeserializeObject<Genre>(responseMessage);
            }

            return View(genre);
        }

        // POST: Genre/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Genre genre)
        {
            if (ModelState.IsValid)
            {
                string createGenreInfo = JsonConvert.SerializeObject(genre);
                StringContent stringContentInfo = new StringContent(createGenreInfo, Encoding.UTF8, "application/json");
                HttpResponseMessage editHttpResponseMessage = _client.PutAsync(_client.BaseAddress + $"api/Genre/{id}", stringContentInfo).Result;
                if (editHttpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(genre);
        }

        // GET: Genre/Delete/5
        public ActionResult Delete(int id)
        {
            Genre genreInfo = new Genre();
            HttpResponseMessage getGenreHttpResponseMessage = _client.GetAsync(_client.BaseAddress + $"api/Genre/{id}").Result;
            if (getGenreHttpResponseMessage.IsSuccessStatusCode)
            {
                genreInfo = getGenreHttpResponseMessage.Content.ReadAsAsync<Genre>().Result;
            }
            return View(genreInfo);
        }

        // POST: Genre/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Genre genre)
        {
            HttpResponseMessage deleteGenreHttpResponseMessage = _client.DeleteAsync(_client.BaseAddress + $"api/Genre/{id}").Result;
            if (deleteGenreHttpResponseMessage.IsSuccessStatusCode)
            {
                //genreInfo = getGenreHttpResponseMessage.Content.ReadAsAsync<Genre>().Result;
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
