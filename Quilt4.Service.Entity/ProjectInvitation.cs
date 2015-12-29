using System;

namespace Quilt4.Service.Entity
{
    public class ProjectInvitation
    {
        public Guid ProjectKey { get; set; }
        public string Name { get; set; }
        public string InvitedByUserName { get; set; }
        public DateTime InviteTime { get; set; }
    }
}