using API.Models;
using System.Collections.Generic;

namespace API.Repositories
{
    public interface ISongRepository
    {
        void InsertSong(Song song);
        void UpdateSong(Song song);
        void DeleteSong(int songid);
        Song GetSongById(int Id);
        IEnumerable<Song> GetSongs();
    }
}
