using System;
using System.Collections.Generic;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Interface.Repository
{
    public interface IRepository
    {
        void CreateProject(string userName, Guid projectKey, string name, string projectApiKey, DateTime createDate, string dashboardColor);

        //TODO: Revisit
        //void SaveUser(User user);
        //User GetUser(string username);
        //void SaveLoginSession(LoginSession loginSession);
        T GetSetting<T>(string name);
        T GetSetting<T>(string name, T defaultValue);
        void SetSetting<T>(string name, T value);
        int GetNextTicket(string projectApiKey, string name, string version, string type, string level, string message, string stackTrace);
        Guid? GetProjectId(string projectApiKey);
        Guid SaveApplication(Guid projectId, string name);
        Guid SaveVersion(Guid applicaitonId, string version, string supportToolkitNameVersion);
        Guid SaveIssueType(Guid versionId, int ticket, string type, string issueLevel, string message, string stackTrace);
        Guid SaveSession(Guid sessionId, DateTime clientStartTime, string callerIp, Guid applicaitonId, Guid versionId, Guid userDataId, Guid machineId, string environment);
        Guid SaveUserData(string fingerprint, string userName);
        Guid SaveMachine(string fingerprint, string name, IDictionary<string, string> data);
        Guid SaveIssue(Guid issueId, Guid issueTypeId, Guid sessionId, DateTime clientTime, IDictionary<string, string> data);
        Session GetSession(Guid sessionId);
        void UpdateProject(Guid projectId, string name, string dashboardColor);
    }
}