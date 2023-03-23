using API.DataDbContext;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly MusicDbContext _dbContext;

        public GenreRepository(MusicDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void DeleteGenre(int genreId)
        {
            var genre = _dbContext.Genres.Find(genreId);
            _dbContext.Genres.Remove(genre);
            Save();
        }

        public Genre GetGenreById(int genreId)
        {
            return _dbContext.Genres.Find(genreId);
        }

        public IEnumerable<Genre> GetGategories()
        {
            return _dbContext.Genres.ToList();
        }

        public void InsertGenre(Genre genre)
        {
            _dbContext.Genres.Add(genre);
            Save();
        }

        public void UpdateGenre(Genre genre)
        {
            _dbContext.Entry(genre).State = EntityState.Modified;
            Save();
        }

        private void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
