using System;
using System.Collections.Generic;

namespace Quilt4.Service.Interface.Business
{
    public interface IInvitationBusiness
    {
        IEnumerable<Entity.ProjectInvitation> GetUserInvitations(string userName);
        void Invite(string userName, Guid projectKey, string user);
        //void Accept(string userName, string inviteCode);
    }
}