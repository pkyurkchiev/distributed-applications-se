using MC.Infrastructure.Messaging.Requests.Genres;
using MC.Infrastructure.Messaging.Requests.Movies;
using MC.Infrastructure.Messaging.Responses.Movies;

namespace MC.ApplicationServices.Interfaces
{
    public interface IMoviesManagementService
    {
        Task<GetMoviesResponse> GetMovies(GetGenresRequest request);
        Task<CreateMovieResponse> CreateMovie(CreateMovieRequest request);
        Task<DeleteMovieResponse> DeleteMovie(DeleteMovieRequest request);
    }
}
