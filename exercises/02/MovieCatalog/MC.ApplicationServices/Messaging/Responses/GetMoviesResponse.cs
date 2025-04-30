namespace MC.ApplicationServices.Messaging.Responses
{
    public class GetMoviesResponse : ServiceResponseBase
    {
        public List<MovieViewModel> Movies { get; set; }
    }
}
