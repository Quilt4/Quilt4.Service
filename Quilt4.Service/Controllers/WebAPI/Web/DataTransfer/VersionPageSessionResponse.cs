using System;

namespace Quilt4.Service.Controllers.WebAPI.Web.DataTransfer
{
    public class VersionPageSessionResponse
    {
        public string SessionKey { get; set; }
        public DateTime ServerStartTime { get; set; }
        public DateTime LastUsedServerTime { get; set; }
        public DateTime? EndServerTime { get; set; }
        public string MachineName { get; set; }
        public string UserName { get; set; }
        public string Environment { get; set; }
        public string CallerIp { get; set; }
    }
}