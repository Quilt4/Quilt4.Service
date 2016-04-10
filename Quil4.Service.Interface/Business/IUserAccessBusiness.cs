using System;
using System.Collections.Generic;

namespace Quilt4.Service.Interface.Business
{
    public interface IUserAccessBusiness
    {
        void AssureAccess(string userName, Guid projectKey);
        void AssureAccess(string userName, IEnumerable<Guid> projectKeys);
    }
}