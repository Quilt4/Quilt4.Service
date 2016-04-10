namespace Quilt4.Service.Entity
{
    public class ProjectMember
    {
        public ProjectMember(string userName, string eMail, bool confirmed, string role, string fullName, string avatarUrl)
        {
            Role = role;
            UserName = userName;
            EMail = eMail;
            Confirmed = confirmed;
            FullName = fullName;
            AvatarUrl = avatarUrl;
        }

        public string UserName { get; }
        public string EMail { get; }
        public bool Confirmed { get; }
        public string Role { get; }
        public string FullName { get; }
        public string AvatarUrl { get; }
    }
}