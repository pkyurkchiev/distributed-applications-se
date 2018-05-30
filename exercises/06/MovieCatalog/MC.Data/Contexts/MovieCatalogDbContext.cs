using MC.Data.Entities;
using System.Data.Entity;

namespace MC.Data.Contexts
{
    public class MovieCatalogDbContext : DbContext
    {
        public MovieCatalogDbContext() 
            //: base(@"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\MovieCatalogDb.mdf;Initial Catalog=MovieCatalogDb;Integrated Security=True")
            : base(@"Data Source=.\sqlexpress;Initial Catalog=MovieCatalogDb;Integrated Security=SSPI;")
        { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}
