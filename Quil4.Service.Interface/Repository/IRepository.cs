﻿using System;
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
        Guid? GetProjectKey(string projectApiKey);
        ProjectPageProject[] GetProjects(string userName);
        void CreateProject(string userName, Guid projectKey, string name, DateTime createTime, string dashboardColor, string projectApiKey);
        void UpdateProject(Guid projectKey, string name, string dashboardColor, DateTime updateTime, string userName);
        void DeleteProject(Guid projectKey);

        //Session
        Session GetSession(Guid sessionId);
        void SetSessionUsed(Guid sessionKey, DateTime serverDateTime);
        void SetSessionEnd(Guid sessionKey, DateTime serverDateTime);
        void CreateSession(Guid sessionKey, DateTime clientStartTime, string callerIp, Guid applicaitonKey, Guid versionKey, Guid? applicationUserKey, Guid? machineKey, string environment, DateTime serverTime);

        //Application/Version
        Guid? GetApplicationKey(Guid projectKey, string name);
        void SaveApplication(Guid applicationKey, Guid projectKey, string name, DateTime serverTime);
        Guid? GetVersionKey(Guid applicaitonKey, string versionName, DateTime? buildTime);
        void SaveVersion(Guid versionKey, Guid applicaitonKey, string versionName, DateTime? buildTime, string supportToolkitNameVersion, DateTime serverTime);

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
    }
}