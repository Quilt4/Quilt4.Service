using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Repository;
using Version = Quilt4.Service.Entity.Version;

namespace Quilt4.Service.MemoryRepository
{
    public class ProjectRepository : IProjectRepository
    {
        public Guid? GetProjectKey(string projectApiKey)
        {
            throw new NotImplementedException();
        }

        public ProjectPageProject[] GetAllProjects()
        {
            throw new NotImplementedException();
        }

        public ProjectPageProject[] GetProjects(string userName)
        {
            throw new NotImplementedException();
        }

        public ProjectInvitation[] GetInvitations()
        {
            throw new NotImplementedException();
        }

        public ProjectInvitation[] GetInvitations(string userName)
        {
            throw new NotImplementedException();
        }

        public ProjectMember[] GetProjectUsers(Guid projectKey)
        {
            throw new NotImplementedException();
        }

        public ProjectMember[] GetProjectInvitation(Guid projectKey)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProjectTarget> GetProjectTargets(Guid projectKey)
        {
            throw new NotImplementedException();
        }
    }

    public class UserRepository : IUserRepository
    {
        public User GetUserByUserKey(string userKey)
        {
            throw new NotImplementedException();
        }

        public User GetUserByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public User GetUserByEMail(string email)
        {
            throw new NotImplementedException();
        }

        public UserInfo GetUserInfo(string userName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserInfo> GetUsers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetRolesByUser(string userName)
        {
            throw new NotImplementedException();
        }

        public Role GetRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public void CreateUser(User user, DateTime serverTime)
        {
            throw new NotImplementedException();
        }

        public void CreateRole(Role role)
        {
            throw new NotImplementedException();
        }

        public void AddUserToRole(string userName, string roleName)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public void AddUserExtraInfo(string userName, string fullName)
        {
            throw new NotImplementedException();
        }
    }

    public class ConfigurationRepository : IConfigurationRepository
    {
        public IEnumerable<Setting> GetSettings()
        {
            throw new NotImplementedException();
        }

        public Setting GetSetting(string settingName)
        {
            return new Setting(settingName, null);
        }

        public void SetSetting(string settingName, string value)
        {
            throw new NotImplementedException();
        }

        public void DeleteSettng(string settingName)
        {
            throw new NotImplementedException();
        }

        public DatabaseInfo GetDatabaseInfo()
        {
            return new DatabaseInfo(false, "", "", -1);
        }
    }

    public class Repository : IRepository
    {
        public Session GetSession(string sessionKey)
        {
            throw new NotImplementedException();
        }

        public void SetSessionUsed(string sessionKey, DateTime serverDateTime)
        {
            throw new NotImplementedException();
        }

        public void SetSessionEnd(string sessionKey, DateTime serverDateTime)
        {
            throw new NotImplementedException();
        }

        public void CreateSession(string sessionKey, DateTime clientStartTime, string callerIp, Guid versionKey, Guid? applicationUserKey, Guid? machineKey, string environment, DateTime serverTime)
        {
            throw new NotImplementedException();
        }

        public Guid? GetApplicationKey(Guid projectKey, string name)
        {
            throw new NotImplementedException();
        }

        public void SaveApplication(Guid applicationKey, Guid projectKey, string name, DateTime serverTime)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Application> GetApplications(Guid projectKey)
        {
            throw new NotImplementedException();
        }

        public Guid? GetVersionKey(Guid applicaitonKey, string versionNumber, DateTime? buildTime)
        {
            throw new NotImplementedException();
        }

        public void SaveVersion(Guid versionKey, Guid applicaitonKey, string versionNumber, DateTime? buildTime, string supportToolkitNameVersion, DateTime serverTime)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Version> GetVersions(Guid applicationKey)
        {
            throw new NotImplementedException();
        }

        public Guid? GetApplicationUser(Guid projectKey, string fingerprint)
        {
            throw new NotImplementedException();
        }

        public void SaveApplicationUser(Guid applicationUserKey, Guid projectKey, string fingerprint, string userName, DateTime serverTime)
        {
            throw new NotImplementedException();
        }

        public Guid? GetMachineKey(Guid projectKey, string fingerprint)
        {
            throw new NotImplementedException();
        }

        public void SaveMachine(Guid machineKey, Guid projectKey, string fingerprint, string name, IDictionary<string, string> data, DateTime serverTime)
        {
            throw new NotImplementedException();
        }

        public Guid? GetIssueTypeKey(Guid versionKey, string type, string issueLevel, string message, string stackTrace)
        {
            throw new NotImplementedException();
        }

        public void CreateIssue(Guid issueKey, Guid issueTypeKey, Guid? issueThreadKey, string sessionKey, DateTime clientTime, IDictionary<string, string> data, DateTime serverTime)
        {
            throw new NotImplementedException();
        }

        public int GetNextTicket(Guid projectKey)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IssueType> GetIssueTypes(Guid versionKey)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RegisterIssueResponseEntity> GetIssues(Guid versionKey)
        {
            throw new NotImplementedException();
        }

        public RegisterIssueResponseEntity GetIssue(Guid issueKey)
        {
            throw new NotImplementedException();
        }

        public void CreateIssueType(Guid issueTypeKey, Guid versionKey, int ticket, string type, string issueLevel, string message, string stackTrace, DateTime serverTime, IssueTypeRequestEntity[] innerIssueTypes)
        {
            throw new NotImplementedException();
        }
    }

    public class SourceRepository : ISourceRepository
    {
        public void LogApiCall(Guid callKey, string sessionKey, Guid? projectKey, DateTime time, TimeSpan elapsed, string callerIp, string currentUserName, string requestType, string path, string request, string response, Guid? issueKey)
        {
            throw new NotImplementedException();
        }

        public void RegisterCommand()
        {
            throw new NotImplementedException();
        }
    }
}
