using System;

namespace Quilt4.Service.Entity
{
    public class RegisterSessionRequestEntity
    {
        public string ProjectApiKey { get; set; }
        public Guid SessionKey { get; set; }
        public DateTime ClientStartTime { get; set; }
        public string Environment { get; set; }
        public ApplicationDataRequestEntity Application { get; set; }
        public MachineDataRequestEntity Machine { get; set; }
        public UserDataRequestEntity User { get; set; }
        public string CallerIp { get; set; }
    }
}