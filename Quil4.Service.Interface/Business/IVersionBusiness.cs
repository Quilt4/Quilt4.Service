using System;
using System.Collections.Generic;
using Quilt4.Service.Entity;
using Version = Quilt4.Service.Entity.Version;

namespace Quilt4.Service.Interface.Business
{
    public interface IVersionBusiness
    {
        VersionPageVersion GetVersion(string userName, Guid projectId, Guid applicationId, Guid versionId);
        IEnumerable<Version> GetVersions(string userName, Guid applicationKey);
        IEnumerable<Version> GetVersions(Guid applicationKey);
    }
}