using System.Collections.Generic;

namespace Quilt4.Service.Entity
{
    public class MachineDataRequestEntity
    {
        public string Fingerprint { get; set; }
        public string Name { get; set; }
        public IDictionary<string, string> Data { get; set; }
    }
}