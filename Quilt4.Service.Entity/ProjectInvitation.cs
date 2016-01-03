using System;

namespace Quilt4.Service.Entity
{
    public class ProjectInvitation
    {
        public ProjectInvitation(Guid projectKey, string name, string invitedByUserName, DateTime inviteTime, string inviteCode, string userName, string userEMail)
        {
            ProjectKey = projectKey;
            Name = name;
            InvitedByUserName = invitedByUserName;
            InviteTime = inviteTime;
            InviteCode = inviteCode;
            UserName = userName;
            UserEMail = userEMail;
        }

        public Guid ProjectKey { get; }
        public string Name { get; }
        public string InvitedByUserName { get; }
        public DateTime InviteTime { get; }
        public string InviteCode { get; }
        public string UserName { get; }
        public string UserEMail { get; }
    }
}