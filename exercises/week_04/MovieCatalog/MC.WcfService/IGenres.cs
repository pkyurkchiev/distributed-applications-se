using MC.ApplicationServices.DTOs;
using System.Collections.Generic;
using System.ServiceModel;

namespace MC.WcfServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IGenres" in both code and config file together.
    [ServiceContract]
    public interface IGenres
    {
        [OperationContract]
        List<GenreDto> GetGenres();

        [OperationContract]
        string PostGenre(GenreDto genreDto);

        [OperationContract]
        string PutGenre(GenreDto genreDto);

        [OperationContract]
        string DeleteGenre(int id);

        [OperationContract]
        string Message();
    }
}
