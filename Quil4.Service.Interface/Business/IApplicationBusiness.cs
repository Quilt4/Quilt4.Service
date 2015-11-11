using System;
using System.Collections.Generic;
using Quilt4.Service.Entity;

namespace Quil4.Service.Interface.Business
{
    public interface IApplicationBusiness
    {
        IEnumerable<Application> GetApplications(string userId, Guid projectId);
        Application GetApplication(string userId, Guid projectId, Guid applicationId);
    }
}