using System;
using System.Collections.Generic;

namespace Quilt4.Service.DataTransfer
{
    public class RegisterIssueRequest
    {
        public string Id { get; set; } //Guid
        public SessionRequest Session { get; set; }
        public DateTime ClientTime { get; set; }
        public IDictionary<string, string> Data { get; set; }
        public IssueTypeRequest IssueType { get; set; }
        public string IssueThreadId { get; set; } //Nullable guid
        public string UserHandle { get; set; }
        public string UserInput { get; set; }
    }

    public class IssueTypeRequest
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string IssueLevel { get; set; }
        public string Type { get; set; }
        public IssueTypeRequest Inner { get; set; }
    }

    public class SessionRequest
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