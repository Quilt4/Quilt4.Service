using System;
using System.Collections.Generic;
using Quilt4.Service.Entity;
using Version = Quilt4.Service.Entity.Version;

namespace Quilt4.Service.Interface.Repository
{
    public interface IRepository
    {
        //User
        void CreateUser(User user, DateTime serverTime);
        void UpdateUser(User user);
        User GetUserByUserKey(string userKey);
        User GetUserByUserName(string userName);
        User GetUserByEMail(string email);
        IEnumerable<User> GetUsers();
        IEnumerable<Role> GetRolesByUser(string userName);
        void AddUserToRole(string userName, string roleName);

        //Role
        void CreateRole(Role role);
        Role GetRole(string roleName);

        //Project
        Guid? GetProjectKey(string projectApiKey);
        ProjectPageProject[] GetProjects(string userName);
        ProjectInvitation[] GetInvitations(string userName);
        void CreateProject(string userName, Guid projectKey, string name, DateTime createTime, string dashboardColor, string projectApiKey);
        void UpdateProject(Guid projectKey, string name, string dashboardColor);
        void DeleteProject(Guid projectKey);
        void CreateProjectInvitation(Guid projectKey, string userName, string inviteCode, string userKey, string email, DateTime serverTime);
        ProjectMember[] GetProjectUsers(Guid projectKey);
        ProjectMember[] GetProjectInvitation(Guid projectKey);

        //Session
        Session GetSession(Guid sessionId);
        void SetSessionUsed(Guid sessionKey, DateTime serverDateTime);
        void SetSessionEnd(Guid sessionKey, DateTime serverDateTime);
        void CreateSession(Guid sessionKey, DateTime clientStartTime, string callerIp, Guid versionKey, Guid? applicationUserKey, Guid? machineKey, string environment, DateTime serverTime);

        //Application/Version
        Guid? GetApplicationKey(Guid projectKey, string name);
        void SaveApplication(Guid applicationKey, Guid projectKey, string name, DateTime serverTime);
        IEnumerable<Application> GetApplications(string userName, Guid projectKey);
        Guid? GetVersionKey(Guid applicaitonKey, string versionNumber, DateTime? buildTime);
        void SaveVersion(Guid versionKey, Guid applicaitonKey, string versionNumber, DateTime? buildTime, string supportToolkitNameVersion, DateTime serverTime);
        IEnumerable<Version> GetVersions(string userName, Guid applicationKey);

        //ApplicationUser
        Guid? GetApplicationUser(Guid projectKey, string fingerprint);
        void SaveApplicationUser(Guid applicationUserKey, Guid projectKey, string fingerprint, string userName, DateTime serverTime);

        //Machine
        Guid? GetMachineKey(Guid projectKey, string fingerprint);
        void SaveMachine(Guid machineKey, Guid projectKey, string fingerprint, string name, IDictionary<string, string> data, DateTime serverTime);

        //Issue
        Guid? GetIssueTypeKey(Guid versionKey, string type, string issueLevel, string message, string stackTrace);
        void CreateIssueType(Guid issueTypeKey, Guid versionKey, int ticket, string type, string issueLevel, string message, string stackTrace, DateTime serverTime);
        void CreateIssue(Guid issueKey, Guid issueTypeKey, Guid sessionKey, DateTime clientTime, IDictionary<string, string> data, DateTime serverTime);
        int GetNextTicket(Guid projectKey);
        IEnumerable<IssueType> GetIssueTypes(string userName, Guid versionKey);
    }
}