using System;
using System.Collections.Generic;
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
        public IEnumerable<MemberResponse> UserInvitationQuery([FromBody] Guid projectKey)
        {
            throw new NotImplementedException("Return members for this project along with pending members.");
        }

        [Route("api/Client/Invitation/InviteCommand")]
        public void InviteCommand(InviteRequest inviteRequest)
        {
            _invitationBusiness.Invite(User.Identity.Name, inviteRequest.ProjectKey, inviteRequest.User);
        }

        [Route("api/Client/Invitation/AcceptCommand")]
        public void AcceptCommand(InviteAcceptRequest inviteRequest)
        {
            //_userBusiness.Accept(User.Identity.Name, inviteRequest.InviteCode);
            throw new NotImplementedException();
        }
    }
}