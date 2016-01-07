namespace Quilt4.Service.Entity
{
    public class UserInfo
    {
        public UserInfo(string userKey, string username, string email, string firstName, string lastName, string avatarUrl)
        {
            UserKey = userKey;
            Username = username;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            AvatarUrl = avatarUrl;
        }

        public string UserKey { get; }
        public string Username { get; }
        public string Email { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string AvatarUrl { get; }
    }
}