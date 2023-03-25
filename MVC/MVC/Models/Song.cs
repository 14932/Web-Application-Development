namespace MVC.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AlbumName { get; set; }
        public string AuthorName { get; set; }
        public Genre SongGenre { get; set; }
        public int GenreId { get; set; }
    }
}
