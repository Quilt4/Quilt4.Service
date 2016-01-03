using System;

namespace Quilt4.Service.Entity
{
    public class Setting
    {
        public Setting(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; }
        public string Value { get; }
    }

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