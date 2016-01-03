namespace Quilt4.Service.Entity
{
    public class ProjectMember
    {
        public ProjectMember(string userName, string eMail, bool confirmed, string role)
        {
            Role = role;
            UserName = userName;
            EMail = eMail;
            Confirmed = confirmed;
        }

        public string UserName { get; }
        public string EMail { get; }
        public bool Confirmed { get; }
        public string Role { get; }
    }
}