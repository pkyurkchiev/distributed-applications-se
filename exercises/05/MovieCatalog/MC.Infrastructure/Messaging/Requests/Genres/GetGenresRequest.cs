using MC.Infrastructure.Messaging.Responses;

namespace MC.Infrastructure.Messaging.Requests.Genres
{
    public class GetGenresRequest : ServiceRequestBase
    {
        public bool IsActive { get; set; }
        public GetGenresRequest(bool isActive)
        {
            IsActive = isActive;
        }
    }
}
