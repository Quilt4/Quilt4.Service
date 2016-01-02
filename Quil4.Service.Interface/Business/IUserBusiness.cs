using System.Collections.Generic;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Interface.Business
{
    public interface IUserBusiness
    {
        IEnumerable<User> GetList();
        IEnumerable<User> SearchUsers(string searchString, string callerIp);
    }
}