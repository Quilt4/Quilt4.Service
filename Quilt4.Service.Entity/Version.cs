using System;

namespace Quilt4.Service.Entity
{
    public class Version
    {
        public Version(Guid versionKey, Guid projectKey, string versionNumber)
        {
            VersionKey = versionKey;
            ProjectKey = projectKey;
            VersionNumber = versionNumber;
        }

        public Guid VersionKey { get; }
        public Guid ProjectKey { get; }
        public string VersionNumber { get; }
    }
}