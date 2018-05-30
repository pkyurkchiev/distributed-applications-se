using MC.ApplicationServices.DTOs;
using MC.ApplicationServices.Implementations;
using System;
using System.Collections.Generic;

namespace MC.WcfServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Genres" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Genres.svc or Genres.svc.cs at the Solution Explorer and start debugging.
    public class Genres : IGenres
    {
        #region Fields
        private GenreManagementService service = new GenreManagementService();
        #endregion

        public List<GenreDto> GetGenres()
        {
            return service.Get();
        }

        public string PostGenre(GenreDto genreDto)
        {
            if(!service.Save(genreDto))
                return "Genre is not inserted";

            return "Genre is inserted";
        }

        public string PutGenre(GenreDto genreDto)
        {
            throw new NotImplementedException();
        }

        public string DeleteGenre(int id)
        {
            if (!service.Delete(id))
                return "Genre is not deleted";

            return "Genre is deleted";
        }

        public string Message()
        {
            return "My first wcf service";
        }
    }
}
