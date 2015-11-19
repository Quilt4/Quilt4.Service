using System;
using System.Collections.Generic;

namespace Quilt4.Service.DataTransfer
{
    public class RegisterSessionRequest
    {
        public string ClientToken { get; set; }
        public string SessionId { get; set; } //Guid
        public DateTime ClientStartTime { get; set; }
        public string Environment { get; set; }
        public ApplicationDataRequest Application { get; set; }
        public MachineDataRequest Machine { get; set; }
        public UserDataRequest User { get; set; }
    }

    public class ApplicationDataRequest
    {
        public string Fingerprint { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string SupportToolkitNameVersion { get; set; }
        public DateTime? BuildTime { get; set; }
    }

    public class MachineDataRequest
    {
        public string Fingerprint { get; set; }
        public string Name { get; set; }
        public IDictionary<string, string> Data { get; set; }
    }

    public class UserDataRequest
    {
        public string Fingerprint { get; set; }
        public string UserName { get; set; }
    }
}