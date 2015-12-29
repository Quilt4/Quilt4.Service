using System;

namespace Quilt4.Service.Entity
{
    public class Version
    {
        public Version(Guid versionKey, string versionNumber)
        {
            VersionKey = versionKey;
            VersionNumber = versionNumber;
        }

        public Guid VersionKey { get; }
        public string VersionNumber { get; }
    }
}