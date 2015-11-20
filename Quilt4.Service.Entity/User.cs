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

        public string Username { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
    }
}