using MC.ApplicationServices.DTOs;
using System.Collections.Generic;
using System.ServiceModel;

namespace MC.WcfServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMovies" in both code and config file together.
    [ServiceContract]
    public interface IMovies
    {

        [OperationContract]
        List<MovieDto> GetMovies();
        
        [OperationContract]
        MovieDto GetMovieByID(int id);

        [OperationContract]
        string PostMovie(MovieDto movieDto);

        [OperationContract]
        string PutMovie(MovieDto movieDto);

        [OperationContract]
        string DeleteMovie(int id);

        [OperationContract]
        string Message();
    }
}
