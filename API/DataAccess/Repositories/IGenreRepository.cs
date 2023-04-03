using API.Models;
using System.Collections.Generic;

namespace API.Repositories
{
    public interface IGenreRepository
    {
        void InsertGenre(Genre genre);
        void UpdateGenre(Genre genre);
        void DeleteGenre(int genreid);
        Genre GetGenreById(int Id);
        IEnumerable<Genre> GetGategories();
    }
}
