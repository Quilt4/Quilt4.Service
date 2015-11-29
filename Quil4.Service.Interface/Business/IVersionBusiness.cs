using System;

namespace Quilt4.Service.Interface.Business
{
    public interface IVersionBusiness
    {
        IVersionPageVersion GetVersion(string userId, Guid projectId, Guid applicationId, Guid versionId);
    }
}