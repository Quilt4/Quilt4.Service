using System;
using System.Collections.Generic;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Interface.Business
{
    public interface IUserBusiness
    {
        IEnumerable<User> GetList();
        void Invite(string userName, Guid projectKey, string user);
    }
}