using MC.ApplicationServices.Messaging.Requests;
using MC.ApplicationServices.Messaging.Responses;

namespace MC.ApplicationServices.Interfaces
{
    public interface IMovieManagementService
    {
        Task<GetMoviesResponse> GetMovies();

        Task<CreateMovieResponse> CreateMovie(CreateMovieRequest request);
    }
}
