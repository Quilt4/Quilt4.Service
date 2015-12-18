using System;
using System.Collections.Generic;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Interface.Repository
{
    public interface IRepository
    {
        void SaveUser(User user);
        User GetUser(string userName);
        ProjectPageProject[] GetProjects(string userName);
        void CreateProject(string userName, Guid projectKey, string name, DateTime createTime, string dashboardColor, string projectApiKey);
        void UpdateProject(Guid projectKey, string name, string dashboardColor, DateTime updateTime, string userName);
        void DeleteProject(Guid projectKey);

        //TODO: Revisit
        void SaveLoginSession(LoginSession loginSession);
        T GetSetting<T>(string name);
        T GetSetting<T>(string name, T defaultValue);
        void SetSetting<T>(string name, T value);
        int GetNextTicket(string clientToken, string type, string message, string stackTrace, string issueLevel, Guid versionId);
        Guid? GetProjectId(string clientToken);
        Guid SaveApplication(Guid projectId, string name);
        Guid SaveVersion(Guid applicaitonId, string version, string supportToolkitNameVersion);
        Guid SaveIssueType(Guid versionId, int ticket, string type, string issueLevel, string message, string stackTrace);
        Guid SaveSession(Guid sessionId, DateTime clientStartTime, string callerIp, Guid applicaitonId, Guid versionId, Guid userDataId, Guid machineId, string environment, DateTime serverTime);
        Guid SaveUserData(string fingerprint, string userName, DateTime updateTime);
        Guid SaveMachine(string fingerprint, string name, IDictionary<string, string> data);
        Guid SaveIssue(Guid issueId, Guid issueTypeId, Guid sessionId, DateTime clientTime, IDictionary<string, string> data);
        Session GetSession(Guid sessionId);
    }
}