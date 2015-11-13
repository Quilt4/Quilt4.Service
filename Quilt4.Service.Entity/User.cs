namespace Quilt4.Service.Entity
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