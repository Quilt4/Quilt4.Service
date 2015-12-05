using System;
using System.Collections.Generic;

namespace Quilt4.Service.Entity
{
    public class RegisterIssueRequestEntity
    {
        public Guid Id { get; set; }
        public Guid SessionId { get; set; }
        public DateTime ClientTime { get; set; }
        public IDictionary<string, string> Data { get; set; }
        public IssueTypeRequestEntity IssueType { get; set; }
        public Guid? IssueThreadId { get; set; }
        public string UserHandle { get; set; }
        public string UserInput { get; set; }
        public string ClientToken { get; set; }
    }

    public class IssueTypeRequestEntity
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string IssueLevel { get; set; }
        public string Type { get; set; }
        public IssueTypeRequestEntity Inner { get; set; }
    }

    public class SessionRequestEntity
    {
        public string ProjectApiKey { get; set; }
        public Guid SessionId { get; set; }
        public DateTime ClientStartTime { get; set; }
        public string Environment { get; set; }
        public ApplicationDataRequestEntity Application { get; set; }
        public MachineDataRequestEntity Machine { get; set; }
        public UserDataRequestEntity User { get; set; }
        public string CallerIp { get; set; }
    }

    public class ApplicationDataRequestEntity
    {
        public string Fingerprint { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        public string SupportToolkitNameVersion { get; set; }
        public DateTime? BuildTime { get; set; }
    }

    public class MachineDataRequestEntity
    {
        public string Fingerprint { get; set; }
        public string Name { get; set; }
        public IDictionary<string, string> Data { get; set; }
    }

    public class UserDataRequestEntity
    {
        public string Fingerprint { get; set; }
        public string UserName { get; set; }
    }
}