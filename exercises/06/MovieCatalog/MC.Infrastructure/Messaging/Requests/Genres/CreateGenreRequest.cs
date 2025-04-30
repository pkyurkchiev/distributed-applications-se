using MC.Infrastructure.Messaging.Responses;

namespace MC.Infrastructure.Messaging.Requests.Genres
{
    public class CreateGenreRequest : ServiceRequestBase
    {
        public GenreModel Genre { get; set; }

        public CreateGenreRequest(GenreModel genre)
        {
            Genre = genre;
        }
    }
}
