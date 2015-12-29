using System;

namespace Quilt4.Service.Entity
{
    public class User
    {
        public User(string userKey, string username, string email, string passwordHash)
        {
            UserKey = userKey;
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
        }

        public string UserKey { get; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
    }
}