using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Models
{
    public class SongGenreViewModel
    {
        public Song Song { get; set; }
        public SelectList Genres { get; set; }
    }
}
