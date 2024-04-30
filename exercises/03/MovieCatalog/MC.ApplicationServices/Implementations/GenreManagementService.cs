using MC.ApplicationServices.Interfaces;
using MC.ApplicationServices.Messaging.Requests;
using MC.ApplicationServices.Messaging.Responses;
using MC.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MC.ApplicationServices.Implementations
{
    public class GenreManagementService : BaseManagementService, IGenreManagementService
    {
        private readonly MovieCatalogDbContext _context;

        public GenreManagementService(ILogger<GenreManagementService> logger, MovieCatalogDbContext context) : base(logger) 
        {
            _context = context;
        }

        public async Task<GetGenresResponse> GetGenres()
        {
            GetGenresResponse response = new() { Genres = new() };
            var genres = await _context.Genres.ToListAsync();

            foreach (var genre in genres)
            {
                response.Genres.Add(new()
                {
                    Name = genre.Name,
                });
            }

            return response;
        }

        public async Task<CreateGenreResponse> CreateGenre(CreateGenreRequest request)
        {
            await _context.Genres.AddAsync(new()
            {
                Name = request.Genre.Name,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = 1,
                IsActivated = true,
            });
            await _context.SaveChangesAsync();
            return new();
        }

        public async Task<DeleteGenreResponse> DeleteGenre(DeleteGenreRequest request)
        {
            var genre = _context.Genres.Find(request.Id);

            if (genre == null)
            {
                _logger.LogError("Genre with identifier {request.Id} not found", request.Id);
                throw new Exception("");
            }

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            return new();
        }
    }
}
