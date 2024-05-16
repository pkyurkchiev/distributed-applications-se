using MC.Infrastructure.Messaging.Responses;

namespace MC.Infrastructure.Messaging.Requests.Movies
{
    public class CreateMovieRequest : ServiceRequestBase
    {
        public MovieModel Movie { get; set; }

        public CreateMovieRequest(MovieModel movie)
        {
            Movie = movie;
        }
    }
}
