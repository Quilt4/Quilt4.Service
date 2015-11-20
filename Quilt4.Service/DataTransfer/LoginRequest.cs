namespace Quilt4.Service.DataTransfer
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string PublicSessionKey { get; set; }
    }
}