using MC.ApplicationServices.Interfaces;
using MC.ApplicationServices.Messaging.Requests;
using MC.ApplicationServices.Messaging.Responses;
using MC.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MC.ApplicationServices.Implementations
{
    public class MovieManagementService : BaseManagementService, IMovieManagementService
    {
        private readonly MovieCatalogDbContext _context;

        public MovieManagementService(ILogger<MovieManagementService> logger, MovieCatalogDbContext context) : base(logger)
        {
            _context = context;
        }

        public async Task<GetMoviesResponse> GetMovies(GetGenresRequest request)
        {
            GetMoviesResponse response = new() { Movies = new() };

            var movies = await _context.Movies.Where(x => x.IsActivated == request.IsActive).Include("Genre").Include("Rating").ToListAsync();

            foreach (var movie in movies)
            {
                response.Movies.Add(new()
                {
                    Title = movie.Title,
                    ReleaseDate = movie.ReleaseDate,
                    Country = movie.Country,
                    Studio = movie.Studio,
                    Genre = movie.Genre.Name
                });
            }

            return response;
        }

        public async Task<CreateMovieResponse> CreateMovie(CreateMovieRequest request)
        {
            if (request.Movie.Title.Length < 3)
            {
                _logger.LogWarning("Title '{title}' length must be greater then 3!", request.Movie.Title);
            }

            await _context.Movies.AddAsync(new()
            {
                Title = request.Movie.Title,
                ReleaseDate = request.Movie.ReleaseDate,
                Country = request.Movie.Country,
                Studio = request.Movie.Studio,
                GenreId = request.Movie.GenreId,
                RatingId = request.Movie.RatingId,
                CreatedOn = DateTime.UtcNow,
                CreatedBy = 1,
                IsActivated = true,
            });
            await _context.SaveChangesAsync();
            return new();
        }

        public async Task<DeleteMovieResponse> DeleteMovie(DeleteMovieRequest request)
        {
            var movie = _context.Movies.Find(request.Id);

            if (movie == null)
            {
                _logger.LogError("Movie with identifier {request.Id} not found", request.Id);
                throw new Exception("");
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return new();
        }
    }
}
