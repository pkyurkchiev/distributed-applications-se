using MC.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MC.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext context;

        public IMoviesRepository Movies { get; set; }

        public IGenresRepository Genres { get; set; }

        public DbContext Context { get { return context; } }

        public UnitOfWork(DbContext context)
        {
            this.context = context;
            Movies = new MoviesRepository(context);
            Genres = new GenresRepository(context);
        }

        public void Dispose() => this.Dispose(true);

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.context?.Dispose();
            }
        }

        public async Task<int> SaveChangesAsync() => await this.context.SaveChangesAsync();        
    }
}
