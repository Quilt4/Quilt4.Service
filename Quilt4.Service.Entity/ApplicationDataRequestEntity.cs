using System;

namespace Quilt4.Service.Entity
{
    public class ApplicationDataRequestEntity
    {
        public string Fingerprint { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string SupportToolkitNameVersion { get; set; }
        public DateTime? BuildTime { get; set; }
    }
}