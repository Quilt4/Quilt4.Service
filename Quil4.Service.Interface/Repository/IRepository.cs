using System;
using System.Collections.Generic;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Interface.Repository
{
    public interface IRepository
    {
        //User
        void SaveUser(User user);
        User GetUser(string userName);

        //Project
        Guid GetProjectKey(string projectApiKey);
        ProjectPageProject[] GetProjects(string userName);
        void CreateProject(string userName, Guid projectKey, string name, DateTime createTime, string dashboardColor, string projectApiKey);
        void UpdateProject(Guid projectKey, string name, string dashboardColor, DateTime updateTime, string userName);
        void DeleteProject(Guid projectKey);

        //Session
        void SetSessionUsed(Guid sessionKey, DateTime serverDateTime);
        void SetSessionEnd(Guid sessionKey, DateTime serverDateTime);
        void CreateSession(Guid sessionKey, DateTime clientStartTime, string callerIp, Guid applicaitonKey, Guid versionKey, Guid? applicationUserKey, Guid? machineKey, string environment, DateTime serverTime);

        //Application/Version
        Guid SaveApplication(Guid projectKey, string name);
        Guid SaveVersion(Guid applicaitonKey, string version, string supportToolkitNameVersion);

        //ApplicationUser
        Guid SaveApplicationUser(Guid projectKey, string fingerprint, string userName, DateTime updateTime);

        //Machine
        Guid SaveMachine(Guid projectKey, string fingerprint, string name, IDictionary<string, string> data);

        //Issue
        Guid SaveIssueType(Guid versionKey, int ticket, string type, string issueLevel, string message, string stackTrace);
        void SaveIssue(Guid issueId, Guid issueTypeId, Guid sessionId, DateTime clientTime, IDictionary<string, string> data);
        int GetNextTicket(Guid projectKey); //, string type, string message, string stackTrace, string issueLevel, Guid versionId);

        //TODO: Revisit
        void SaveLoginSession(LoginSession loginSession);
        T GetSetting<T>(string name);
        T GetSetting<T>(string name, T defaultValue);
        void SetSetting<T>(string name, T value);
        Session GetSession(Guid sessionId);
    }
}