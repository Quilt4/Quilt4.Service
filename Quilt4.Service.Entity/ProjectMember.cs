namespace Quilt4.Service.Entity
{
    public class ProjectMember
    {
        public ProjectMember(string userName, string eMail, bool confirmed, string role, string firstName, string lastName, string avatarUrl)
        {
            Role = role;
            UserName = userName;
            EMail = eMail;
            Confirmed = confirmed;
            FirstName = firstName;
            LastName = lastName;
            AvatarUrl = avatarUrl;
        }

        public string UserName { get; }
        public string EMail { get; }
        public bool Confirmed { get; }
        public string Role { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string AvatarUrl { get; }
    }
}