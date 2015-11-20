namespace Quilt4.Service.Entity
{
    public class LoginSession
    {
        public LoginSession(string publicKey, string privateKey)
        {
            PrivateKey = privateKey;
            PublicKey = publicKey;
        }

        public string PublicKey { get; private set; }
        public string PrivateKey { get; private set; }
    }
}