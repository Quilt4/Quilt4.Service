namespace Quilt4.Service.DataTransfer
{
    public class LoginResponse
    {
        public string SecurityType { get; set; }
        public string PublicSessionKey { get; set; }
        public string PrivateSessionKey { get; set; }
    }
}