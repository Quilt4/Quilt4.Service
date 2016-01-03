using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Quilt4.Service.Interface.Business;
using Quilt4Net.Core.DataTransfer;

namespace Quilt4.Service.Controllers.Client
{
    [Authorize]
    public class ClientInvitationController : ApiController
    {
        private readonly IInvitationBusiness _invitationBusiness;

        public ClientInvitationController(IInvitationBusiness invitationBusiness)
        {
            _invitationBusiness = invitationBusiness;
        }

        [HttpPost]
        [Route("api/Client/Invitation/UserInvitationQuery")]
        public IEnumerable<InvitationResponse> UserInvitationQuery()
        {
            var response = _invitationBusiness.GetUserInvitations(User.Identity.Name).Select(x => new InvitationResponse
            {
                ProjectKey = x.ProjectKey,
                ProjectName = x.Name,
                InviteCode = x.InviteCode,
                InvitedByUserName = x.InvitedByUserName,
                InviteTime = x.InviteTime,
                UserName = x.UserName,
                UserEMail = x.UserEMail
            });
            return response;
        }

        [Route("api/Client/Invitation/InviteCommand")]
        public void InviteCommand(InviteRequest inviteRequest)
        {
            _invitationBusiness.Invite(User.Identity.Name, inviteRequest.ProjectKey, inviteRequest.User);
        }

        [Route("api/Client/Invitation/AcceptCommand")]
        public void AcceptCommand(InviteAcceptRequest inviteRequest)
        {
            _invitationBusiness.Accept(User.Identity.Name, inviteRequest.InviteCode);
        }
    }

    public class InvitationResponse
    {
        //TODO: Replace this class with the version from the nuget package
        public Guid ProjectKey { get; set; }
        public string ProjectName { get; set; }
        public string InviteCode { get; set; }
        public DateTime InviteTime { get; set; }
        public string InvitedByUserName { get; set; }
        public string UserName { get; set; }
        public string UserEMail { get; set; }
    }
}