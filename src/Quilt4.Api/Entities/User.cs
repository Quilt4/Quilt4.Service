namespace Quilt4.Api.Entities
{
    public class User
    {
        public User(string username, string email, string passwordHash)
        {
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
        }

        public string Username { get; }
        public string Email { get; }
        public string PasswordHash { get; }
    }
}