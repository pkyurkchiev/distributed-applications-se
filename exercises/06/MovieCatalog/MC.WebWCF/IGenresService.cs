using CoreWCF;
using MC.ApplicationServices.Interfaces;
using MC.Infrastructure.Messaging.Responses.Genres;
using System;
using System.Runtime.Serialization;

namespace MC.WebWCF
{
    [ServiceContract]
    public interface IGenresService
    {
        [OperationContract]
        Task<GetGenresResponse> GetGenres();
    }

    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class GenresService : IGenresService
    {
        private readonly IGenresManagementService _genresManagementService;


        public GenresService(IGenresManagementService genresManagementService)
        {
            _genresManagementService = genresManagementService;
        }

        public async Task<GetGenresResponse> GetGenres()
        {
            return await _genresManagementService.GetGenres();
        }
    }
}
