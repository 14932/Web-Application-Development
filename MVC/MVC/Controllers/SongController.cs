using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using MVC.Models;

namespace MVC.Controllers
{
    public class SongController : Controller
    {
        public const string baseUrl = "http://localhost:27169/";
        private readonly Uri _clientBaseAddress = new Uri(baseUrl);
        private readonly HttpClient _client;

        public SongController()
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

        // GET: Song
        public async Task<ActionResult> Index()
        {
            List<Song> songs = new List<Song>();
            HeaderClearing();

            HttpResponseMessage httpResponseMessage = await _client.GetAsync("api/Song");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                songs = JsonConvert.DeserializeObject<List<Song>>(responseMessage);
            }
            return View(songs);
        }

        // GET: Song/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Song song = new Song();
            HeaderClearing();

            HttpResponseMessage httpResponseMessage = await _client.GetAsync($"api/Song/{id}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                song = JsonConvert.DeserializeObject<Song>(responseMessage);
            }
            return View(song);
        }

        // GET: Song/Create
        public async Task<ActionResult> CreateAsync()
        {
            List<Genre> genres = new List<Genre>();
            HeaderClearing();
            HttpResponseMessage httpResponseMessage = await _client.GetAsync("api/Genre");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                genres = JsonConvert.DeserializeObject<List<Genre>>(responseMessage);
            }

            var viewModel = new SongGenreViewModel
            {
                Song = new Song(),
                Genres = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(genres, "Id", "Name")
            };
            return View(viewModel);
        }

        // POST: Song/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Song song)
        {
            song.SongGenre = new Genre { Id = song.GenreId };
            if (ModelState.IsValid)
            {
                string createSongInfo = JsonConvert.SerializeObject(song);
                StringContent stringContentInfo = new StringContent(createSongInfo, Encoding.UTF8, "application/json");
                HttpResponseMessage createHttpResponseMessage = _client.PostAsync(_client.BaseAddress + "api/Song", stringContentInfo).Result;
                if (createHttpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(song);
        }

        // GET: Song/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Song song = new Song();
            HeaderClearing();

            HttpResponseMessage httpResponseMessage = await _client.GetAsync($"api/Song/{id}");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                song = JsonConvert.DeserializeObject<Song>(responseMessage);
            }

            List<Genre> genres = new List<Genre>();
            httpResponseMessage = await _client.GetAsync("api/Genre");

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseMessage = httpResponseMessage.Content.ReadAsStringAsync().Result;
                genres = JsonConvert.DeserializeObject<List<Genre>>(responseMessage);
            }
            var viewModel = new SongGenreViewModel
            {
                Song = song,
                Genres = new SelectList(genres, "Id", "Name", song.GenreId)
            };
            return View(viewModel);
        }

        // POST: Song/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SongGenreViewModel songGenreModel)
        {
            songGenreModel.Song.SongGenre = new Genre { Id = songGenreModel.Song.GenreId };
            if (ModelState.IsValid)
            {
                string createSongInfo = JsonConvert.SerializeObject(songGenreModel.Song);
                StringContent stringContentInfo = new StringContent(createSongInfo, Encoding.UTF8, "application/json");
                HttpResponseMessage editHttpResponseMessage = _client.PutAsync(_client.BaseAddress + $"api/Song/{id}", stringContentInfo).Result;
                if (editHttpResponseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(songGenreModel);
        }

        // GET: Song/Delete/5
        public ActionResult Delete(int id)
        {
            Song songInfo = new Song();
            HttpResponseMessage getSongHttpResponseMessage = _client.GetAsync(_client.BaseAddress + $"api/Song/{id}").Result;
            if (getSongHttpResponseMessage.IsSuccessStatusCode)
            {
                songInfo = getSongHttpResponseMessage.Content.ReadAsAsync<Song>().Result;
            }
            return View(songInfo);
        }

        // POST: Song/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Song song)
        {
            HttpResponseMessage deleteSongHttpResponseMessage = _client.DeleteAsync(_client.BaseAddress + $"api/Song/{id}").Result;
            if (deleteSongHttpResponseMessage.IsSuccessStatusCode)
            {
                //songInfo = getSongHttpResponseMessage.Content.ReadAsAsync<Song>().Result;
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
