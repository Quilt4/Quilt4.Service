using System;
using System.Collections.Generic;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Interface.Business
{
    public interface IVersionBusiness
    {
        VersionPageVersion GetVersion(string userName, Guid projectId, Guid applicationId, Guid versionId);
        IEnumerable<Entity.Version> GetVersions(string userName, Guid applicationKey);
    }
}