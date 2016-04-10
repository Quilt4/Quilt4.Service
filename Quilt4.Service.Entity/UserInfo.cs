namespace Quilt4.Service.Entity
{
    public class UserInfo
    {
        public UserInfo(string userKey, string username, string email, string fullName, string avatarUrl)
        {
            UserKey = userKey;
            Username = username;
            Email = email;
            FullName = fullName;
            AvatarUrl = avatarUrl;
        }

        public string UserKey { get; }
        public string Username { get; }
        public string Email { get; }
        public string FullName { get; }
        public string AvatarUrl { get; }
    }
}