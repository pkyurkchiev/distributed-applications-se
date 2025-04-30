using MC.ApplicationServices.Messaging.Responses;

namespace MC.ApplicationServices.Messaging.Requests
{
    public class CreateGenreRequest : ServiceRequestBase
    {
        public GenreModel Genre {get; set;}

        public CreateGenreRequest(GenreModel genre)
        {
            Genre = genre;
        }
    }
}
