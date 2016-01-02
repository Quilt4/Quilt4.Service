using System;
using System.Collections.Generic;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Interface.Business
{
    public interface IInvitationBusiness
    {
        void Invite(string userName, Guid projectKey, string user);
        //void Accept(string userName, string inviteCode);
    }

    public interface IUserBusiness
    {
        IEnumerable<User> GetList();
        IEnumerable<User> SearchUsers(string searchString, string callerIp);
    }
}