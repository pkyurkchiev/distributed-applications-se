using MC.ApplicationServices.Messaging.Requests;
using MC.ApplicationServices.Messaging.Responses;

namespace MC.ApplicationServices.Interfaces
{
    public interface IGenresManagementService
    {
        Task<GetGenresResponse> GetGenres();
        Task<CreateGenreResponse> CreateGenre(CreateGenreRequest request);
        Task<DeleteGenreResponse> DeleteGenre(DeleteGenreRequest request);
    }
}
