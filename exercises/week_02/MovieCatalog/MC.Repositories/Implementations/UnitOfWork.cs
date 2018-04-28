using MC.Data.Contexts;
using MC.Data.Entities;
using System;

namespace MC.Repositories.Implementations
{
    public class UnitOfWork : IDisposable
    {
        private MovieCatalogDbContext context = new MovieCatalogDbContext();
        private GenericRepository<Movie> movieRepository;
        private GenericRepository<Genre> genreReposiotry;

        public GenericRepository<Movie> MovieRepository
        {
            get
            {

                if (this.movieRepository == null)
                {
                    this.movieRepository = new GenericRepository<Movie>(context);
                }
                return movieRepository;
            }
        }

        public GenericRepository<Genre> GenreReposiotry
        {
            get
            {
                if (this.genreReposiotry == null)
                {
                    this.genreReposiotry = new GenericRepository<Genre>(context);
                }
                return genreReposiotry;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
