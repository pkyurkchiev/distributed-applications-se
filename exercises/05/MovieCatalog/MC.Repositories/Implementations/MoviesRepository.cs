using MC.Data.Entities;
using MC.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MC.Repositories.Implementations
{
    public class MoviesRepository : Repository<Movie>, IMoviesRepository
    {
        public MoviesRepository(DbContext context) : base(context) { }

        public override async Task<IEnumerable<Movie>> GetAllAsync(bool isActive = true)
        {
            return await SoftDeleteQueryFilter(this.DbSet, isActive).Include("Genre").Include("Rating").ToListAsync();
        }
    }
}
