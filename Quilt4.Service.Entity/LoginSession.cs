namespace Quilt4.Service.Entity
{
    public class LoginSession
    {
        public LoginSession(string publicKey, string privateKey)
        {
            PrivateKey = privateKey;
            PublicKey = publicKey;
        }

        public string PublicKey { get; }
        public string PrivateKey { get; }
    }
}