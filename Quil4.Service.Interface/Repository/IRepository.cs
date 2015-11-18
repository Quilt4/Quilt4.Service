using System;
using System.Collections.Generic;
using Quilt4.Service.Entity;

namespace Quil4.Service.Interface.Repository
{
    public interface IRepository
    {
        void SaveUser(User user);
        User GetUser(string username);
        void SaveLoginSession(LoginSession loginSession);
        T GetSetting<T>(string name);
        void SetSetting<T>(string name, T value);
        int GetNextTicket(string clientToken, string name, string version, string type, string level, string message, string stackTrace);
        Guid? GetProjectId(string clientToken);
        Guid SaveApplication(Guid projectId, string name);
        Guid SaveVersion(Guid applicaitonId, string version, string supportToolkitNameVersion);
        Guid SaveIssueType(Guid versionId, int ticket, string type, string issueLevel, string message, string stackTrace);
        Guid SaveSession(Guid sessionId, DateTime clientStartTime, string callerIp);
        Guid SaveUserData(string fingerprint, string userName);
        Guid SaveMachine(string fingerprint, string name, IDictionary<string, string> data);
        Guid SaveIssue(Guid issueId, Guid issueTypeId, Guid sessionId, Guid userDataId, Guid machineId, DateTime clientTime, string environment, IDictionary<string, string> data);
    }
}