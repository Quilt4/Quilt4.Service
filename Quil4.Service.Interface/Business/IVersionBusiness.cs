using System;
using Quilt4.Service.Entity;

namespace Quil4.Service.Interface.Business
{
    public interface IVersionBusiness
    {
        VersionPageVersion GetVersion(string userId, Guid projectId, Guid applicationId, Guid versionId);
    }
}