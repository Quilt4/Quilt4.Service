using System;
using System.Collections.Generic;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Repository.MemoryRepository
{
    public class MemoryRespository : IRepository
    {
        public void SaveUser(User user)
        {
            throw new NotImplementedException();
        }

        public User GetUser(string username)
        {
            throw new NotImplementedException();
        }

        public void SaveLoginSession(LoginSession loginSession)
        {
            throw new NotImplementedException();
        }

        public T GetSetting<T>(string name)
        {
            throw new NotImplementedException();
        }

        public T GetSetting<T>(string name, T defaultValue)
        {
            throw new NotImplementedException();
        }

        public void SetSetting<T>(string name, T value)
        {
            throw new NotImplementedException();
        }

        public int GetNextTicket(string clientToken, string type, string message, string stackTrace, string issueLevel, Guid versionId)
        {
            throw new NotImplementedException();
        }

        public Guid? GetProjectId(string clientToken)
        {
            throw new NotImplementedException();
        }

        Guid IRepository.SaveApplication(Guid projectId, string name)
        {
            throw new NotImplementedException();
        }

        public Guid SaveVersion(Guid applicaitonId, string version, string supportToolkitNameVersion)
        {
            throw new NotImplementedException();
        }

        public Guid SaveIssueType(Guid versionId, int ticket, string type, string issueLevel, string message, string stackTrace)
        {
            throw new NotImplementedException();
        }

        public Guid SaveSession(Guid sessionId, DateTime clientStartTime, string callerIp, Guid applicaitonId, Guid versionId, Guid userDataId, Guid machineId, string environment)
        {
            throw new NotImplementedException();
        }

        public Guid SaveUserData(string fingerprint, string userName)
        {
            throw new NotImplementedException();
        }

        public Guid SaveMachine(string fingerprint, string name, IDictionary<string, string> data)
        {
            throw new NotImplementedException();
        }

        public Guid SaveIssue(Guid issueId, Guid issueTypeId, Guid sessionId, DateTime clientTime, IDictionary<string, string> data)
        {
            throw new NotImplementedException();
        }

        public Session GetSession(Guid sessionId)
        {
            throw new NotImplementedException();
        }

        public void CreateProject(Guid projectKey, string name, string dashboardColor)
        {
            throw new NotImplementedException();
        }

        public Guid CreateProject(string name, string dashboardColor)
        {
            throw new NotImplementedException();
        }

        public void UpdateProject(Guid projectId, string name, string dashboardColor)
        {
            throw new NotImplementedException();
        }
    }
}