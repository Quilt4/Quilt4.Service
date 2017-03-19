using System;

namespace Quilt4.Service.Entity
{
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
}