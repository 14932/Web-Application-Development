using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.DataDbContext
{
    public class MusicDbContext : DbContext
    {
        public MusicDbContext(DbContextOptions<MusicDbContext> options) : base(options) { Database.EnsureCreated(); }

        public DbSet<Song> Songs { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}
