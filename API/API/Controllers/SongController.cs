using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly ISongRepository _songRepository;

        public SongController(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }
        // GET: api/<SongController>
        [HttpGet]
        public IActionResult Get()
        {
            var songs = _songRepository.GetSongs();
            return new OkObjectResult(songs);
        }

        // GET api/<SongController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var song = _songRepository.GetSongById(id);
            return new OkObjectResult(song);
        }

        // POST api/<SongController>
        [HttpPost]
        public IActionResult Post([FromBody] Song song)
        {
            if (song == null)
            {
                return new NoContentResult();
            }
            using (var scope = new TransactionScope())
            {
                _songRepository.InsertSong(song);
                scope.Complete();
                return CreatedAtAction(nameof(Get), new { id = song.Id }, song);
            }
        }

        // PUT api/<SongController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Song song)
        {
            if (song == null)
            {
                return new NoContentResult();
            }
            using (var scope = new TransactionScope())
            {
                _songRepository.UpdateSong(song);
                scope.Complete();
                return new OkResult();
            }
        }

        // DELETE api/<SongController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _songRepository.DeleteSong(id);
            return new OkResult();
        }
    }
}
