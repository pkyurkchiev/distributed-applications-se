namespace MC.Infrastructure.Messaging.Requests.Genres
{
    public class DeleteGenreRequest : IntegerServiceRequestBase
    {
        public DeleteGenreRequest(int id) : base(id)
        {
        }
    }
}
