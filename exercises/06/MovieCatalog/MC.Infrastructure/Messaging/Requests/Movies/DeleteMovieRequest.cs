namespace MC.Infrastructure.Messaging.Requests.Movies
{
    public class DeleteMovieRequest : IntegerServiceRequestBase
    {
        public DeleteMovieRequest(int id) : base(id)
        {
        }
    }
}
