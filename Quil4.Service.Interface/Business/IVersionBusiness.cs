using System;
using System.Collections.Generic;
using Version = Quilt4.Service.Entity.Version;

namespace Quil4.Service.Interface.Business
{
    public interface IVersionBusiness
    {
        IEnumerable<Version> GetVersions(string userId, Guid projectId, Guid applicationId);
        Version GetVersion(string userId, Guid projectId, Guid applicationId, Guid versionId);
    }
}