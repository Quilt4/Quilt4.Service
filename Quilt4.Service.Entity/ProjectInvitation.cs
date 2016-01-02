using System;

namespace Quilt4.Service.Entity
{
    public class ProjectInvitation
    {
        public ProjectInvitation(Guid projectKey, string name, string invitedByUserName, DateTime inviteTime, string inviteCode)
        {
            ProjectKey = projectKey;
            Name = name;
            InvitedByUserName = invitedByUserName;
            InviteTime = inviteTime;
            InviteCode = inviteCode;
        }

        public Guid ProjectKey { get; }
        public string Name { get; }
        public string InvitedByUserName { get; }
        public DateTime InviteTime { get; }
        public string InviteCode { get; }
    }
}