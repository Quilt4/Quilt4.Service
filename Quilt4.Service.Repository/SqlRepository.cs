using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Transactions;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Repository;
using Quilt4.Service.SqlRepository.Extensions;
using Quilt4.Service.SqlRepository.Converters;
using Quilt4Net;

namespace Quilt4.Service.SqlRepository
{    
    public class SqlRepository : IRepository
    {
        public void CreateUser(Entity.User user, DateTime serverTime)
        {
            using (var context = GetDataContext())
            {
                var dbUser = new User
                {
                    UserKey = user.UserKey,
                    UserName = user.Username,
                    Email = user.Email,
                    EmailConfirmed = false,
                    PasswordHash = user.PasswordHash,
                    CreateServerTime = serverTime,
                };
                context.Users.InsertOnSubmit(dbUser);
                context.SubmitChanges();
            }
        }

        public void UpdateUser(Entity.User user)
        {
            using (var context = GetDataContext())
            {
                var dbUser = context.Users.Single(x => x.UserKey == user.UserKey);
                dbUser.UserName = user.Username;
                dbUser.Email = user.Email;
                dbUser.PasswordHash = user.PasswordHash;
                context.SubmitChanges();
            }
        }

        public Entity.User GetUserByUserName(string username)
        {
            using (var context = GetDataContext())
            {
                var dbUser = context.Users.SingleOrDefault(x => x.UserName.ToLower() == username.ToLower());
                return dbUser == null ? null : new Entity.User(dbUser.UserKey, dbUser.UserName, dbUser.Email, dbUser.PasswordHash);
            }
        }

        public Entity.User GetUserByEMail(string email)
        {
            using (var context = GetDataContext())
            {
                var dbUser = context.Users.SingleOrDefault(x => x.Email == email);
                return dbUser == null ? null : new Entity.User(dbUser.UserKey, dbUser.UserName, dbUser.Email, dbUser.PasswordHash);
            }
        }

        public Entity.User GetUserByUserKey(string userKey)
        {
            using (var context = GetDataContext())
            {
                var dbUser = context.Users.SingleOrDefault(x => x.UserKey == userKey);
                return dbUser == null ? null : new Entity.User(dbUser.UserKey, dbUser.UserName, dbUser.Email, dbUser.PasswordHash);
            }
        }

        public IEnumerable<UserInfo> GetUsers()
        {
            using (var context = GetDataContext())
            {
                return context.Users.Select(dbUser => new UserInfo(dbUser.UserKey, dbUser.UserName, dbUser.Email, dbUser.FullName, dbUser.AvatarUrl)).ToArray();
            }
        }

        public IEnumerable<Entity.Role> GetRolesByUser(string userName)
        {
            using (var context = GetDataContext())
            {
                var response = context.Roles.Where(x => context.UserRoles.Any(y => y.User.UserName == userName)).ToArray();
                return response.Select(x => new Entity.Role(x.RoleName));
            }
        }

        public void AddUserToRole(string userName, string roleName)
        {
            using (var context = GetDataContext())
            {
                context.UserRoles.InsertOnSubmit(new UserRole
                {
                    UserId = context.Users.Single(x => x.UserName == userName).UserId,
                    RoleId = context.Roles.Single(x => x.RoleName == roleName).RoleId,
                });
                context.SubmitChanges();
            }
        }

        public void CreateRole(Entity.Role role)
        {
            using (var context = GetDataContext())
            {
                context.Roles.InsertOnSubmit(new Role { RoleName = role.RoleName });
                context.SubmitChanges();
            }
        }

        public Entity.Role GetRole(string roleName)
        {
            using (var context = GetDataContext())
            {
                var response = context.Roles.FirstOrDefault(x => x.RoleName == roleName);
                return response == null ? null : new Entity.Role(response.RoleName);
            }
        }

        public void DeleteProject(Guid projectKey)
        {
            using (var context = GetDataContext())
            {
                var project = context.Projects.Single(x => x.ProjectKey == projectKey);
                context.Projects.DeleteOnSubmit(project);
                context.SubmitChanges();

                //TODO: Also delete from all read-tables
            }
        }

        public void CreateProjectInvitation(Guid projectKey, string userName, string inviteCode, string userKey, string email, DateTime serverTime)
        {
            using (var context = GetDataContext())
            {
                var user = context.Users.SingleOrDefault(x => x.UserKey == userKey);

                context.ProjectInvitations.InsertOnSubmit(new ProjectInvitation
                {
                    ProjectId = context.Projects.Single(x => x.ProjectKey == projectKey).ProjectId,
                    InviteCode = inviteCode,
                    ServerCreateTime = serverTime,
                    UserEmail = email,
                    UserId = user?.UserId,
                    InviterUserId = context.Users.Single(x => x.UserName == userName).UserId,                    
                });
                context.SubmitChanges();
            }
        }

        public void DeleteProjectInvitation(Guid projectKey, string userName)
        {
            using (var context = GetDataContext())
            {
                var projectInvitation = context.ProjectInvitations.Single(x => x.User.UserName == userName && x.Project.ProjectKey == projectKey);
                context.ProjectInvitations.DeleteOnSubmit(projectInvitation);
                context.SubmitChanges();
            }
        }

        public void AddProjectMember(string userName, Guid projectKey, string role)
        {
            using (var context = GetDataContext())
            {
                context.ProjectUsers.InsertOnSubmit(new ProjectUser
                {
                    ProjectId = context.Projects.Single(x => x.ProjectKey == projectKey).ProjectId,
                    UserId = context.Users.Single(x => x.UserName == userName).UserId,
                    Role = role
                });
                context.SubmitChanges();
            }
        }

        public ProjectMember[] GetProjectUsers(Guid projectKey)
        {
            using (var context = GetDataContext())
            {
                return context.Users.Where(x => context.ProjectUsers.Any(y => y.UserId == x.UserId && y.Project.ProjectKey == projectKey)).Select(x => new ProjectMember(x.UserName, x.Email, true, x.ProjectUsers.Single(y => y.UserId == x.UserId && y.Project.ProjectKey == projectKey).Role, x.FullName, x.AvatarUrl)).ToArray();
            }
        }

        public ProjectMember[] GetProjectInvitation(Guid projectKey)
        {
            using (var context = GetDataContext())
            {
                return context.Users.Where(x => context.ProjectInvitations.Any(y => y.UserId == x.UserId && y.Project.ProjectKey == projectKey)).Select(x => new ProjectMember(x.UserName, x.Email, false, x.ProjectUsers.Single(y => y.UserId == x.UserId && y.Project.ProjectKey == projectKey).Role, x.FullName, x.AvatarUrl)).ToArray();
            }
        }

        public int GetNextTicket(Guid projectKey)
        {
            using (var context = GetDataContext())
            {
                int ticket;

                using (var scope = new TransactionScope())
                {
                    var project = context.Projects.Single(x => x.ProjectKey == projectKey);

                    project.LastTicket++;
                    ticket = project.LastTicket;
                    context.SubmitChanges();
                    scope.Complete();
                }

                return ticket;
            }
        }

        public IEnumerable<RegisterIssueResponseEntity> GetIssues(Guid versionKey)
        {
            using (var context = GetDataContext())
            {
                return context.Issues
                    .Where(x => x.IssueType.Version.VersionKey == versionKey)
                    .Select(x => new RegisterIssueResponseEntity(x.IssueKey, x.IssueType.Ticket, x.CreationServerTime, x.IssueType.Version.Application.Project.ProjectKey, x.IssueType.IssueTypeKey)).ToArray();
            }
        }
        
        public IEnumerable<Entity.IssueType> GetIssueTypes(Guid versionKey)
        {
            using (var context = GetDataContext())
            {
                return context.IssueTypes
                    .Where(x => x.Version.VersionKey == versionKey)
                    .Select(x => new Entity.IssueType(x.IssueTypeKey, x.Version.Application.Project.ProjectKey, x.Version.VersionKey, x.IssueTypeDetail.Type, x.Level, x.IssueTypeDetail.Message, x.IssueTypeDetail.StackTrace, x.Ticket, x.CreationServerTime)).ToArray();
            }
        }

        public IEnumerable<Entity.Setting> GetSettings()
        {
            using (var context = GetDataContext())
            {
                return context.Settings.Select(x => new Entity.Setting(x.Name, x.Value)).ToArray();
            }
        }

        public Entity.Setting GetSetting(string settingName)
        {
            using (var context = GetDataContext())
            {
                var result = context.Settings.SingleOrDefault(x => x.Name == settingName);
                if (result == null) return null;
                return new Entity.Setting(result.Name, result.Value);
            }
        }

        public void SetSetting(string settingName, string value)
        {
            using (var context = GetDataContext())
            {
                var setting = context.Settings.SingleOrDefault(x => x.Name == settingName);
                if (setting == null)
                {
                    setting = new Setting { Name = settingName };
                    context.Settings.InsertOnSubmit(setting);
                }

                setting.Value = value;

                context.SubmitChanges();
            }
        }

        public void DeleteSettng(string settingName)
        {
            using (var context = GetDataContext())
            {
                var setting = context.Settings.SingleOrDefault(x => x.Name == settingName);
                if (setting == null) return;
                context.Settings.DeleteOnSubmit(setting);
                context.SubmitChanges();
            }
        }

        public void AddUserExtraInfo(string userName, string fullName, string defaultAvatarUrl)
        {
            using (var context = GetDataContext())
            {
                var user = context.Users.SingleOrDefault(x => x.UserName == userName);
                if (user == null) return;

                user.FullName = fullName;
                user.AvatarUrl = defaultAvatarUrl;

                context.SubmitChanges();
            }
        }

        public UserInfo GetUserInfo(string userName)
        {
            using (var context = GetDataContext())
            {
                var user = context.Users.SingleOrDefault(x => x.UserName == userName);
                if (user == null) return null;
                return new UserInfo(user.UserKey, user.UserName, user.Email, user.FullName, user.AvatarUrl);
            }
        }
        
        public DatabaseInfo GetDatabaseInfo()
        {
            try
            {
                using (var context = GetDataContext())
                {
                    var version = context.DBVersions.Max(x => x.VersionNumber);

                    var response = new DatabaseInfo(true, context.Connection.DataSource, context.Connection.Database, version);
                    return response;
                }
            }
            catch (Exception)
            {
                return new DatabaseInfo(false, null, null, -1);
            }
        }

        public Guid? GetProjectKey(string projectApiKey)
        {
            using (var context = GetDataContext())
            {
                var project = context.Projects.SingleOrDefault(x => x.ProjectApiKey == projectApiKey);
                return project?.ProjectKey;
            }
        }

        public Guid? GetApplicationKey(Guid projectKey, string name)
        {
            using (var context = GetDataContext())
            {
                var application = context.Applications.SingleOrDefault(x => x.Project.ProjectKey == projectKey && x.Name == name);
                return application?.ApplicationKey;
            }
        }

        public void SaveApplication(Guid applicationKey, Guid projectKey, string name, DateTime serverTime)
        {
            using (var context = GetDataContext())
            {
                var newApplication = new Application
                {
                    ApplicationKey = applicationKey,
                    ProjectId = context.Projects.Single(x => x.ProjectKey == projectKey).ProjectId,
                    Name = name,
                    CreationServerTime = serverTime,                    
                };

                context.Applications.InsertOnSubmit(newApplication);
                context.SubmitChanges();
            }
        }

        public IEnumerable<Entity.Application> GetApplications(Guid projectKey)
        {
            using (var context = GetDataContext())
            {
                return context.Applications.Where(x => x.Project.ProjectKey == projectKey).Select(x => x.ToApplication()).ToArray();
            }
        }

        public IEnumerable<Entity.Application> GetApplications(string userName, Guid projectKey)
        {
            using (var context = GetDataContext())
            {
                return context.Applications.Where(x => x.Project.User.UserName == userName && x.Project.ProjectKey == projectKey).Select(x => x.ToApplication()).ToArray();
            }
        }

        public Guid? GetVersionKey(Guid applicaitonKey, string versionNumber, DateTime? buildTime)
        {
            using (var context = GetDataContext())
            {
                var version = context.Versions.SingleOrDefault(x => x.Application.ApplicationKey == applicaitonKey && x.VersionNumber == versionNumber);
                return version?.VersionKey;
            }
        }

        public void SaveVersion(Guid versionKey, Guid applicaitonKey, string versionNumber, DateTime? buildTime, string supportToolkitNameVersion, DateTime serverTime)
        {
            using (var context = GetDataContext())
            {
                var newVersion = new Version
                {
                    VersionKey = versionKey,
                    ApplicationId = context.Applications.Single(x => x.ApplicationKey == applicaitonKey).ApplicationId,
                    VersionNumber = versionNumber,
                    SupportToolkitVersion = supportToolkitNameVersion,
                    CreationServerTime = serverTime,
                    BuildTime = buildTime,                    
                };

                context.Versions.InsertOnSubmit(newVersion);
                context.SubmitChanges();
            }
        }

        public IEnumerable<Entity.Version> GetVersions(string userName, Guid applicationKey)
        {
            using (var context = GetDataContext())
            {
                return context.Versions.Where(x => x.Application.Project.User.UserName == userName && x.Application.ApplicationKey == applicationKey).Select(x => x.ToVersion()).ToArray();
            }
        }

        public IEnumerable<Entity.Version> GetVersions(Guid applicationKey)
        {
            using (var context = GetDataContext())
            {
                return context.Versions.Where(x => x.Application.ApplicationKey == applicationKey).Select(x => x.ToVersion()).ToArray();
            }
        }

        public Guid? GetIssueTypeKey(Guid versionKey, string type, string issueLevel, string message, string stackTrace)
        {
            using (var context = GetDataContext())
            {
                var issueType =
                    context.IssueTypes.SingleOrDefault(
                        x =>
                            x.Version.VersionKey == versionKey && 
                            x.IssueTypeDetail.Type.Equals(type) && 
                            x.Level.Equals(issueLevel) &&
                            x.IssueTypeDetail.Message.Equals(message) &&
                            (stackTrace == null ? x.IssueTypeDetail.StackTrace == null : x.IssueTypeDetail.StackTrace == stackTrace));

                return issueType?.IssueTypeKey;
            }
        }

        public void CreateIssueType(Guid issueTypeKey, Guid versionKey, int ticket, string type, string issueLevel, string message, string stackTrace, DateTime serverTime, IssueTypeRequestEntity[] innerIssueTypes)
        {
            using (var context = GetDataContext())
            {
                var itk = GetIssueTypeKey(issueTypeKey, type, issueLevel, message, stackTrace);
                if (itk != null) throw new InvalidOperationException("A IssueType with this signature already exists.");

                var detail = new IssueTypeDetail
                {
                    Type = type,
                    Message = message,
                    StackTrace = stackTrace,                        
                };
                context.IssueTypeDetails.InsertOnSubmit(detail);

                var newIssueType = new IssueType
                {
                    IssueTypeKey = issueTypeKey,
                    VersionId = context.Versions.Single(x => x.VersionKey == versionKey).VersionId,
                    Ticket = ticket,
                    Level = issueLevel,
                    CreationServerTime = serverTime,
                    IssueTypeDetail = detail
                };
                context.IssueTypes.InsertOnSubmit(newIssueType);

                CreateIssueTypeDetail(innerIssueTypes, detail, context);

                context.SubmitChanges();
            }
        }

        private static void CreateIssueTypeDetail(IssueTypeRequestEntity[] innerIssueTypes, IssueTypeDetail detail, Quilt4DataContext context)
        {
            if (innerIssueTypes == null) return;

            foreach (var issueTypeRequestEntity in innerIssueTypes)
            {
                if (issueTypeRequestEntity == null) return;

                var issueTypeDetail = new IssueTypeDetail
                {
                    Type = issueTypeRequestEntity.Type,
                    Message = issueTypeRequestEntity.Message,
                    StackTrace = issueTypeRequestEntity.StackTrace,
                    IssueTypeDetail1 = detail
                };
                context.IssueTypeDetails.InsertOnSubmit(issueTypeDetail);

                CreateIssueTypeDetail(issueTypeRequestEntity.Inner, issueTypeDetail, context);
            }
        }

        public void SetSessionEnd(string sessionKey, DateTime serverDateTime)
        {
            using (var context = GetDataContext())
            {
                var session = context.Sessions.Single(x => x.SessionKey == sessionKey);
                session.EndServerTime = serverDateTime;
                context.SubmitChanges();
            }
        }

        public void SetSessionUsed(string sessionKey, DateTime serverDateTime)
        {
            using (var context = GetDataContext())
            {
                try
                {
                    var session = context.Sessions.SingleOrDefault(x => x.SessionKey == sessionKey);
                    if (session == null)
                    {
                        throw new InvalidOperationException("There is no session with provided key.").AddData("sessionKey", sessionKey);
                    }
                    session.LastUsedServerTime = serverDateTime;
                    context.SubmitChanges();
                }
                catch (ChangeConflictException)
                {
                    //Some other thread changed the session, ignore this change.
                }
            }
        }

        public void CreateSession(string sessionKey, DateTime clientStartTime, string callerIp, Guid versionKey, Guid? applicationUserKey, Guid? machineKey, string environment, DateTime serverTime)
        {
            using (var context = GetDataContext())
            {
                var newSession = new Session
                {
                    SessionKey = sessionKey,
                    CallerIp = callerIp,
                    StartClientTime = clientStartTime,
                    StartServerTime = serverTime,
                    LastUsedServerTime = serverTime,
                    EndServerTime = null,
                    ApplicationUserId = applicationUserKey != null ? context.ApplicationUsers.Single(x => x.ApplicationUserKey == applicationUserKey).ApplicationUserId : (int?)null,
                    MachineId = machineKey != null ? context.Machines.Single(x => x.MachineKey == machineKey).MachineId : (int?)null,
                    Enviroment = environment,
                    VersionId = context.Versions.Single(x => x.VersionKey == versionKey).VersionId,                    
                };

                context.Sessions.InsertOnSubmit(newSession);
                context.SubmitChanges();
            }
        }

        public Guid? GetApplicationUser(Guid projectKey, string fingerprint)
        {
            using (var context = GetDataContext())
            {
                try
                {
                    var userData = context.ApplicationUsers.SingleOrDefault(x => x.Fingerprint == fingerprint && x.Project.ProjectKey == projectKey);
                    return userData?.ApplicationUserKey;
                }
                catch (Exception exception)
                {
                    exception.Data.Add("Fingerprint", fingerprint);
                    exception.Data.Add("ProjectKey", projectKey);
                    throw;
                }
            }
        }

        public void SaveApplicationUser(Guid applicationUserKey, Guid projectKey, string fingerprint, string userName, DateTime serverTime)
        {
            using (var context = GetDataContext())
            {
                var applicationUser = new ApplicationUser
                {
                    ApplicationUserKey = applicationUserKey,
                    ProjectId = context.Projects.Single(x => x.ProjectKey == projectKey).ProjectId,
                    Fingerprint = fingerprint,
                    UserName = userName,
                    CreationServerTime = serverTime,                    
                };

                context.ApplicationUsers.InsertOnSubmit(applicationUser);
                context.SubmitChanges();
            }
        }

        public Guid? GetMachineKey(Guid projectKey, string fingerprint)
        {
            using (var context = GetDataContext())
            {
                var machine = context.Machines.SingleOrDefault(x => x.Project.ProjectKey == projectKey && x.Fingerprint == fingerprint);
                return machine?.MachineKey;
            }
        }

        //TODO: Make this method a bit more clean
        public void SaveMachine(Guid machineKey, Guid projectKey, string fingerprint, string name, IDictionary<string, string> data, DateTime serverTime)
        {
            using (var context = GetDataContext())
            {
                //    var machine = context.Machines.SingleOrDefault(x => x.Fingerprint == fingerprint);

                //    if (machine != null)
                //    {
                //        var needToSubmitChanges = false;

                //        if (machine.Name != name)
                //        {
                //            machine.Name = name;
                //            machine.LastUpdateDate = DateTime.UtcNow;
                //            needToSubmitChanges = true;
                //        }

                //        //To improve performance ?
                //        var machineDatas = machine.MachineDatas.ToArray();

                //        if (data != null)
                //        {
                //            //Add missing and update existing machineDatas
                //            foreach (var d in data)
                //            {
                //                var match = machineDatas.SingleOrDefault(x => x.Name == d.Key);

                //                if (match == null)
                //                {
                //                    var newMachineData = new MachineData
                //                    {
                //                        Id = Guid.NewGuid(),
                //                        MachineId = machine.Id,
                //                        CreationDate = DateTime.UtcNow,
                //                        LastUpdateDate = DateTime.UtcNow,
                //                        Name = d.Key,
                //                        Value = d.Value
                //                    };

                //                    context.MachineDatas.InsertOnSubmit(newMachineData);

                //                    needToSubmitChanges = true;

                //                    continue;
                //                }

                //                if (match.Value != d.Value)
                //                {
                //                    match.Value = d.Value;
                //                    match.LastUpdateDate = DateTime.UtcNow;

                //                    needToSubmitChanges = true;
                //                }
                //            }
                //        }

                //        //Remove old machineDatas
                //        foreach (var machineData in machineDatas)
                //        {
                //            var match = data.SingleOrDefault(x => x.Key == machineData.Name);

                //            if (match.Equals(new KeyValuePair<string, string>()))
                //            {
                //                context.MachineDatas.DeleteOnSubmit(machineData);
                //                needToSubmitChanges = true;
                //            }
                //        }


                //        if (needToSubmitChanges)
                //            context.SubmitChanges();

                //        return machine.Id;
                //    }

                var newMachine = new Machine
                {
                    MachineKey = machineKey,
                    Fingerprint = fingerprint,
                    Name = name,
                    CreationServerTime = serverTime,
                    ProjectId = context.Projects.Single(x => x.ProjectKey == projectKey).ProjectId,
                };

                context.Machines.InsertOnSubmit(newMachine);
                context.SubmitChanges();

                if (data != null)
                {
                    foreach (var d in data)
                    {
                        var machineData = new MachineData
                        {
                            MachineId = newMachine.MachineId,                            
                            Name = d.Key,
                            Value = d.Value,
                            CreationServerTime = serverTime,                            
                        };

                        context.MachineDatas.InsertOnSubmit(machineData);
                    }
                }

                context.SubmitChanges();
            }
        }

        public void CreateIssue(Guid issueKey, Guid issueTypeKey, Guid? issueThreadKey, string sessionKey, DateTime clientTime, IDictionary<string, string> data, DateTime serverTime)
        {
            using (var context = GetDataContext())
            {
                var issue = new Issue
                {
                    IssueKey = issueKey,
                    IssueTypeId = context.IssueTypes.Single(x => x.IssueTypeKey == issueTypeKey).IssueTypeId,
                    CreationClientTime = clientTime,
                    CreationServerTime = serverTime,
                    SessionId = context.Sessions.Single(x => x.SessionKey == sessionKey).SessionId,
                    IssueThreadKey = issueThreadKey,
                };

                context.Issues.InsertOnSubmit(issue);
                context.SubmitChanges();

                if (data != null)
                {
                    foreach (var d in data)
                    {
                        var issueData = new IssueData
                        {
                            IssueId = issue.IssueId,
                            Name = d.Key,
                            Value = d.Value,                             
                        };

                        context.IssueDatas.InsertOnSubmit(issueData);
                    }
                }

                context.SubmitChanges();
            }
        }

        public Entity.Session GetSession(string sessionKey)
        {
            using (var context = GetDataContext())
            {
                var session = context.Sessions.SingleOrDefault(x => x.SessionKey == sessionKey);
                return session.ToSessionEntity();
            }
        }

        public void CreateProject(string userName, Guid projectKey, string name, DateTime createTime, string dashboardColor, string projectApiKey)
        {
            using (var context = GetDataContext())
            {
                var user = context.Users.Single(x => x.UserName == userName);

                var project = new Project
                {
                    ProjectKey = projectKey,
                    Name = name,
                    DashboardColor = dashboardColor,
                    CreationServerTime = createTime,
                    ProjectApiKey = projectApiKey,
                    OwnerUserId = user.UserId,
                    LastTicket = 0,                    
                };

                context.Projects.InsertOnSubmit(project);
                context.SubmitChanges();

                var projectUser = new ProjectUser
                {
                    ProjectId = context.Projects.Single(x => x.ProjectKey == projectKey).ProjectId,
                    UserId = user.UserId,
                    Role = "Owner"
                };

                context.ProjectUsers.InsertOnSubmit(projectUser);
                context.SubmitChanges();
            }
        }

        public void UpdateProject(Guid projectKey, string name, string dashboardColor)
        {
            using (var context = GetDataContext())
            {                
                var project = context.Projects.Single(x => x.ProjectKey == projectKey);

                project.Name = name;
                project.DashboardColor = dashboardColor;

                context.SubmitChanges();
            }
        }

        public Entity.ProjectInvitation[] GetInvitations()
        {
            using (var context = GetDataContext())
            {
                return context.ProjectInvitations.Select(x => new Entity.ProjectInvitation(x.Project.ProjectKey, x.Project.Name, x.User.UserName, x.ServerCreateTime, x.InviteCode, x.User1 != null ? x.User1.UserName : null, x.UserEmail)).ToArray();
            }
        }

        public Entity.ProjectInvitation[] GetInvitations(string userName)
        {
            using (var context = GetDataContext())
            {
                return context.ProjectInvitations.Where(x => x.User.UserName == userName).Select(x => new Entity.ProjectInvitation(x.Project.ProjectKey, x.Project.Name, x.User.UserName, x.ServerCreateTime, x.InviteCode, x.User1 != null ? x.User1.UserName : null, x.UserEmail)).ToArray();
            }
        }

        public ProjectPageProject[] GetAllProjects()
        {
            using (var context = GetDataContext())
            {
                var projectPageApplicaitons = context.Projects;
                return projectPageApplicaitons.Select(x => x.ToProjectPageProject()).ToArray();
            }
        }

        public ProjectPageProject[] GetProjects(string userName)
        {
            using (var context = GetDataContext())
            {
                var projectPageApplicaitons = context.Projects.Where(x => x.User.UserName == userName || x.ProjectUsers.Any(y => y.User.UserName == userName && x.ProjectId == y.ProjectId));
                return projectPageApplicaitons.Select(x => x.ToProjectPageProject()).ToArray();
            }
        }

        private static Quilt4DataContext GetDataContext()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            return new Quilt4DataContext(connectionString);
        }
    }
}