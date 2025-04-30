using MC.ApplicationServices.Interfaces;
using MC.Infrastructure.Messaging.Requests.Genres;
using MC.Infrastructure.Messaging.Requests.Movies;
using MC.Infrastructure.Messaging.Responses.Movies;
using MC.Data.Contexts;
using MC.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MC.ApplicationServices.Implementations
{
    public class MoviesManagementService : BaseManagementService, IMoviesManagementService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MoviesManagementService(ILogger<MoviesManagementService> logger, IUnitOfWork unitOfWork) : base(logger)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetMoviesResponse> GetMovies(GetGenresRequest request)
        {
            GetMoviesResponse response = new() { Movies = new() };

            var movies = await _unitOfWork.Movies.GetAllAsync(request.IsActive);

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

            _unitOfWork.Movies.Insert(new()
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
            await _unitOfWork.SaveChangesAsync();
            return new();
        }

        public async Task<DeleteMovieResponse> DeleteMovie(DeleteMovieRequest request)
        {
            var movie = await _unitOfWork.Movies.GetByIdAsync(request.Id);

            if (movie == null)
            {
                _logger.LogError("Movie with identifier {request.Id} not found", request.Id);
                throw new Exception("");
            }

            _unitOfWork.Movies.Delete(movie);
            await _unitOfWork.SaveChangesAsync();

            return new();
        }
    }
}
