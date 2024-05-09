using MC.ApplicationServices.Messaging.Requests;
using MC.ApplicationServices.Messaging.Responses;

namespace MC.ApplicationServices.Interfaces
{
    public interface IMoviesManagementService
    {
        Task<GetMoviesResponse> GetMovies(GetGenresRequest request);
        Task<CreateMovieResponse> CreateMovie(CreateMovieRequest request);
        Task<DeleteMovieResponse> DeleteMovie(DeleteMovieRequest request);
    }
}
