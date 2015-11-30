using System;
using System.Collections.Generic;
using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Entity
{
    public class VersionPageIssueType
    {
        public Guid Id { get; set; }
        public int Ticket { get; set; }
        public string Type { get; set; }
        public int Issues { get; set; }
        public string Level { get; set; }
        public DateTime? LastIssue { get; set; }
        public IEnumerable<string> Enviroments { get; set; }
        public string Message { get; set; }
    }

    //public class RegisterSessionCommandInput : IRegisterSessionCommandInput
    //{
    //    public RegisterSessionCommandInput(Guid sessionId, string projectApiKey, IApplication application, DateTime clientStartTime, string environment, string callerIp)
    //    {
    //        SessionId = sessionId;
    //        ProjectApiKey = projectApiKey;
    //        Application = application;
    //        ClientStartTime = clientStartTime;
    //        Environment = environment;
    //        CallerIp = callerIp;
    //    }

    //    public Guid SessionId { get; }
    //    public string ProjectApiKey { get; }
    //    public IApplication Application { get; }
    //    public IUser User { get; set; }
    //    public IMachine Machine { get; set; }
    //    public DateTime ClientStartTime { get; }
    //    public string Environment { get; }
    //    public string CallerIp { get; }
    //}

    public class CrateUserCommandInput : ICrateUserCommandInput
    {
        public CrateUserCommandInput(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; }
    }
}