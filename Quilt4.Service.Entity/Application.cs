using System;

namespace Quilt4.Service.Entity
{
    public class Application
    {
        public Application(Guid applicationKey, string name)
        {
            ApplicationKey = applicationKey;
            Name = name;
        }

        public Guid ApplicationKey { get; }
        public string Name { get; }
    }

    public interface IIssueRead
    {
        string ProjectName { get; }
        string ApplicationName { get; }
        string VersionNumber { get; }
        string MachineName { get; }
        int Ticket { get; }
        string StackTrace { get; }
        Guid IssueKey { get; }
        string Type { get; }
        string Message { get; }
        string Level { get; }
    }

    public class IssueRead : IIssueRead
    {
        public string ProjectName { get; set; }
        public string ApplicationName { get; set; }
        public string VersionNumber { get; set; }
        public string MachineName { get; set; }
        public int Ticket { get; set; }
        public string StackTrace { get; set; }
        public Guid IssueKey { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string Level { get; set; }
    }
}