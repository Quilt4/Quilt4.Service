using System;

namespace Quilt4.Service.Entity
{
    public class Role
    {
        public Role(string roleName)
        {
            RoleName = roleName;
        }

        public string RoleName { get; }
    }

    public class IssueType
    {
        public IssueType(Guid issueTypeKey, Guid versionKey, string type, string level, string message, string stackTrace, int ticket, DateTime creationServerTime, DateTime lastIssueServerTime)
        {
            IssueTypeKey = issueTypeKey;
            VersionKey = versionKey;
            Type = type;
            Level = level;
            Message = message;
            StackTrace = stackTrace;
            Ticket = ticket;
            CreationServerTime = creationServerTime;
            LastIssueServerTime = lastIssueServerTime;
        }

        public Guid IssueTypeKey { get; }
        public Guid VersionKey { get; }
        public string Type { get; }
        public string Level { get; }
        public string Message { get; }
        public string StackTrace { get; }
        public int Ticket { get; }
        public DateTime CreationServerTime { get; }
        public DateTime LastIssueServerTime { get; }
    }

    public class User
    {
        public User(string userKey, string username, string email, string passwordHash)
        {
            UserKey = userKey;
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
        }

        public string UserKey { get; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
    }
}