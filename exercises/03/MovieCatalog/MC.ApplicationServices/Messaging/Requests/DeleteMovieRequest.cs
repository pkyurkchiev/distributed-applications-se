namespace MC.ApplicationServices.Messaging.Requests
{
    public class DeleteMovieRequest : IntegerServiceRequestBase
    {
        public DeleteMovieRequest(int id) : base(id)
        {
        }
    }
}
