using System;
using System.Collections.Generic;
using Quil4.Service.Interface.Repository;
using Quilt4.Service.Entity;

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

        public void SetSetting<T>(string name, T value)
        {
            throw new NotImplementedException();
        }

        public int GetNextTicket(string clientToken, string name, string version, string type, string level, string message, string stackTrace)
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

        public Guid SaveSession(Guid sessionId, DateTime clientStartTime, string callerIp)
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

        Guid IRepository.SaveIssue(Guid typeId, Guid issueTypeId, Guid sessionId, Guid userDataId, Guid machineId, DateTime clientTime, string environment, IDictionary<string, string> data)
        {
            throw new NotImplementedException();
        }
    }
}