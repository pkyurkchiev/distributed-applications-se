using MC.ApplicationServices.Interfaces;
using MC.ApplicationServices.Messaging.Requests;
using MC.ApplicationServices.Messaging.Responses;
using MC.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Xml.Serialization;

namespace MC.ApplicationServices.Implementations
{
    public class MovieManagementService : IMovieManagementService
    {
        private readonly MovieCatalogDbContext _context;

        public MovieManagementService(MovieCatalogDbContext context)
        {
            _context = context;
        }

        public async Task<GetMoviesResponse> GetMovies()
        {
            GetMoviesResponse response = new() { Movies = new() };
            var movies = await _context.Movies.Include("Genre").Include("Rating").ToListAsync();

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
    }
}
