namespace MC.ApplicationServices.Interfaces
{
    public interface IJwtAuthenticationManager
    {
        string? Authenticate(string clientId, string secret);
    }
}
