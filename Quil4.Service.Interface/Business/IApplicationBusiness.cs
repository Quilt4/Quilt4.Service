using System;
using System.Collections.Generic;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Interface.Business
{
    public interface IApplicationBusiness
    {
        IEnumerable<Application> GetApplications(Guid projectKey);
        IEnumerable<Application> GetApplications(string userName, Guid projectKey);
    }
}