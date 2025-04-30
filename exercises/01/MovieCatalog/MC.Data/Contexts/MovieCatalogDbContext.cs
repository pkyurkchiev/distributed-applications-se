using MC.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MC.Data.Contexts
{
    public class MovieCatalogDbContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public MovieCatalogDbContext(DbContextOptions<MovieCatalogDbContext> options) : base(options) { }
    }
}
