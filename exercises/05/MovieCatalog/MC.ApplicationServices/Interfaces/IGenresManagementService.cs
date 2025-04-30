using MC.Infrastructure.Messaging.Requests.Genres;
using MC.Infrastructure.Messaging.Responses.Genres;

namespace MC.ApplicationServices.Interfaces
{
    public interface IGenresManagementService
    {
        Task<GetGenresResponse> GetGenres();
        Task<CreateGenreResponse> CreateGenre(CreateGenreRequest request);
        Task<DeleteGenreResponse> DeleteGenre(DeleteGenreRequest request);
    }
}
