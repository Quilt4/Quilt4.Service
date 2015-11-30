using System;
using System.Collections.Generic;
using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Interface.Repository
{
    public interface IDataRepository
    {
        void CreateUser(string userName);
        void CreateProject(string userName, Guid projectKey, string name, string projectApiKey, DateTime createDate, string dashboardColor);
        void UpdateProject(string userName, Guid projectKey, string name, DateTime lastUpdateDate, string dashboardColor);

        Guid? GetProjectKeyByProjectApiKey(string projectApiKey);
        void SaveApplication(Guid applicationKey, Guid projectKey, string name);
        void SaveVersion(Guid versionKey, Guid applicaitonKey, string version, string supportToolkitNameVersion);
        void SaveUserData(Guid userDataKey, string fingerprint, string userName);
        void SaveMachine(Guid machineKey, string fingerprint, string name, IDictionary<string, string> data);
        void SaveSession(Guid sessionKey, DateTime clientStartTime, string callerIp, Guid applicaitonKey, Guid versionKey, Guid userDataKey, Guid machineKey, string environment);

        ////TODO: Revisit
        ////void SaveUser(User user);
        ////User GetUser(string username);
        ////void SaveLoginSession(LoginSession loginSession);
        //T GetSetting<T>(string name);
        //T GetSetting<T>(string name, T defaultValue);
        //void SetSetting<T>(string name, T value);
        //int GetNextTicket(string projectApiKey, string name, string version, string type, string level, string message, string stackTrace);
        //Guid? GetProjectId(string projectApiKey);
        //Guid SaveIssueType(Guid versionId, int ticket, string type, string issueLevel, string message, string stackTrace);
        //Guid SaveIssue(Guid issueId, Guid issueTypeId, Guid sessionId, DateTime clientTime, IDictionary<string, string> data);
        //Session GetSession(Guid sessionId);
    }
}