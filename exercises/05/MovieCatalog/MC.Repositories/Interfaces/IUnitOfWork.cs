using Microsoft.EntityFrameworkCore;

namespace MC.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }

        IMoviesRepository Movies { get; }

        IGenresRepository Genres { get; }

        Task<int> SaveChangesAsync();
    }
}
