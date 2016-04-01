using System;

namespace Quilt4.Service.Controllers.WebAPI.Web.DataTransfer
{
    public class ProjectInvitationResponse
    {
        public Guid ProjectKey { get; set; }
        public string Name { get; set; }
        public string InvitedByUserName { get; set; }
        public DateTime InviteTime { get; set; }
    }
}