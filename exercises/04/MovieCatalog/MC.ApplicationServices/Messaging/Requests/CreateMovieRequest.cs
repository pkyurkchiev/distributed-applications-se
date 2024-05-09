using MC.ApplicationServices.Messaging.Responses;

namespace MC.ApplicationServices.Messaging.Requests
{
    public class CreateMovieRequest : ServiceRequestBase
    {
        public MovieModel Movie {get; set;}

        public CreateMovieRequest(MovieModel movie)
        {
            Movie = movie;
        }
    }
}
