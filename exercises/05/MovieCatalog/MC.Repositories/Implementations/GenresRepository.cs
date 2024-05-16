using MC.Data.Entities;
using MC.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MC.Repositories.Implementations
{
    public class GenresRepository : Repository<Genre>, IGenresRepository
    {
        public GenresRepository(DbContext context) : base(context) { }
    }
}
