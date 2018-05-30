using MC.ApplicationServices.DTOs;
using MC.ApplicationServices.Implementations;
using System;
using System.Collections.Generic;

namespace MC.WcfServices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Movies" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Movies.svc or Movies.svc.cs at the Solution Explorer and start debugging.
    public class Movies : IMovies
    {
        #region Fields
        private MovieManagementService service = new MovieManagementService();
        #endregion

        public List<MovieDto> GetMovies()
        {
            return service.Get();
        }

        public MovieDto GetMovieByID(int id)
        {
            return service.GetById(id);
        }

        public string PostMovie(MovieDto movieDto)
        {
            if(!service.Save(movieDto))
                return "Movie is not inserted";

            return "Movie is inserted";
        }

        public string PutMovie(MovieDto movieDto)
        {
            if (!service.Save(movieDto))
                return "Movie is not updated";

            return "Movie is updated";
        }

        public string DeleteMovie(int id)
        {
            if (!service.Delete(id))
                return "Movie is not deleted";

            return "Movie is deleted";
        }

        public string Message()
        {
            return "My first wcf service";
        }
    }
}
