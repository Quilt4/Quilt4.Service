namespace Quilt4.Api.Entities
{
    public class LoginSession
    {
        public LoginSession(string sessionKey)
        {
            SessionKey = sessionKey;
        }

        public string SessionKey { get; }
    }
}