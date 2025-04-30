namespace MC.Infrastructure.Messaging.Responses.Authentications
{
    public class AuthenticationResponse : ServiceResponseBase
    {
        public string Token { get; set; }

        public AuthenticationResponse(string token)
        {
            Token = token;
        }
    }
}
