using API.DataDbContext;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API.Repositories
{
    public class SongRepository : ISongRepository
    {
        private readonly MusicDbContext _dbContext;

        public SongRepository(MusicDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteSong(int songId)
        {
            var song = _dbContext.Songs.Find(songId);
            _dbContext.Songs.Remove(song);
            Save();
        }

        public Song GetSongById(int songId)
        {
            var song = _dbContext.Songs.Find(songId);
            _dbContext.Entry(song).Reference(s => s.SongGenre).Load();
            return song;
        }

        public IEnumerable<Song> GetSongs()
        {
            return _dbContext.Songs.Include(s => s.SongGenre).ToList();
        }

        public void InsertSong(Song song)
        {
            song.SongGenre = _dbContext.Genres.Find(song.SongGenre.Id);
            _dbContext.Songs.Add(song);
            Save();
        }

        public void UpdateSong(Song song)
        {
            _dbContext.Entry(song).State = EntityState.Modified;
            Save();
        }

        private void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
