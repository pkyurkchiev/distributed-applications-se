using MC.Data.Entities;
using System.Data.Entity;

namespace MC.Data.Contexts
{
    public class MovieCatalogDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}
