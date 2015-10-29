namespace Quilt4.Api.Entities
{
    public class LoginSession
    {
        public LoginSession(string sessionKey, string sharedSecret)
        {
            SessionKey = sessionKey;
            SharedSecret = sharedSecret;
        }

        public string SessionKey { get; }
        public string SharedSecret { get; }
    }
}