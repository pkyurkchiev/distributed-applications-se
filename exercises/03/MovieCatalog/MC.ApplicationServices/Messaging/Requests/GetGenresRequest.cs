using MC.ApplicationServices.Messaging.Responses;

namespace MC.ApplicationServices.Messaging.Requests
{
    public class GetGenresRequest : ServiceRequestBase
    {
        public bool IsActive {get; set;}
        public GetGenresRequest(bool isActive)
        {
            IsActive = isActive;
        }
    }
}
