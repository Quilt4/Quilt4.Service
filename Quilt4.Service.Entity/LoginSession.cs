namespace Quilt4.Service.Entity
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