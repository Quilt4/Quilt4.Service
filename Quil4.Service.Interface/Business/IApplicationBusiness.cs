using System;
using System.Collections.Generic;

namespace Quilt4.Service.Interface.Business
{
    public interface IApplicationBusiness
    {
        IEnumerable<Entity.Application> GetApplications(string userName, Guid projectKey);
    }
}