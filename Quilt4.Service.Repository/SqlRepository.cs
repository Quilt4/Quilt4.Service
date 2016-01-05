using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Transactions;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Repository;
using Quilt4.Service.SqlRepository.Extensions;

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
                context.SubmitChanges();
            }
        }

        public Entity.User GetUserByUserName(string username)
        {
            using (var context = GetDataContext())
            {
                var dbUser = context.Users.SingleOrDefault(x => x.UserName == username);
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

        public IEnumerable<Entity.User> GetUsers()
        {
            using (var context = GetDataContext())
            {
                return context.Users.Select(dbUser => new Entity.User(dbUser.UserKey, dbUser.UserName, dbUser.Email, dbUser.PasswordHash)).ToArray();
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

        public Entity.ProjectMember[] GetProjectUsers(Guid projectKey)
        {
            using (var context = GetDataContext())
            {
                return context.Users.Where(x => context.ProjectUsers.Any(y => y.UserId == x.UserId && y.Project.ProjectKey == projectKey)).Select(x => new Entity.ProjectMember(x.UserName, x.Email, true, x.ProjectUsers.Single(y => y.UserId == x.UserId && y.Project.ProjectKey == projectKey).Role, x.FirstName, x.LastName, x.AvatarUrl)).ToArray();
            }
        }

        public Entity.ProjectMember[] GetProjectInvitation(Guid projectKey)
        {
            using (var context = GetDataContext())
            {
                return context.Users.Where(x => context.ProjectInvitations.Any(y => y.UserId == x.UserId && y.Project.ProjectKey == projectKey)).Select(x => new Entity.ProjectMember(x.UserName, x.Email, false, x.ProjectUsers.Single(y => y.UserId == x.UserId && y.Project.ProjectKey == projectKey).Role, x.FirstName, x.LastName, x.AvatarUrl)).ToArray();
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

        public IEnumerable<Entity.IssueType> GetIssueTypes(string userName, Guid versionKey)
        {
            using (var context = GetDataContext())
            {
                return context.IssueTypes.Where(x => x.Version.Application.Project.User.UserName == userName && x.Version.VersionKey == versionKey).Select(x => new Entity.IssueType(x.IssueTypeKey, x.Version.VersionKey, x.Type, x.Level, x.Message, x.StackTrace, x.Ticket, x.CreationServerTime)).ToArray();
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
                return context.Settings.Select(x => new Entity.Setting(x.Name, x.Value)).Single(x => x.Name == settingName);
            }
        }

        public void SetSetting(string settingName, string value)
        {
            using (var context = GetDataContext())
            {
                var val = context.Settings.SingleOrDefault(x => x.Name == settingName);
                if (val == null)
                {
                    val = new Setting { Name = settingName };
                    context.Settings.InsertOnSubmit(val);
                }

                val.Value = value;

                context.SubmitChanges();
            }
        }

        public void AddUserExtraInfo(string id, string firstName, string lastName, string defaultAvatarUrl)
        {
            using (var context = GetDataContext())
            {
                var user = context.Users.SingleOrDefault(x => x.UserKey == id);

                if (user == null) return;

                user.FirstName = firstName;
                user.LastName = lastName;
                user.AvatarUrl = defaultAvatarUrl;

                context.SubmitChanges();
            }
        }

        public ProjectMember GetUser(string name)
        {
            using (var context = GetDataContext())
            {
                var user = context.Users.SingleOrDefault(x => x.UserName == name);

                return new ProjectMember(user.UserName, user.Email, false, null, user.FirstName, user.LastName, user.AvatarUrl);
            }
        }

        public IEnumerable<ProjectMember> GetUsersStartingWith(string email)
        {
            using (var context = GetDataContext())
            {
                //TODO: Sorry, bör byta från projectMemeber
                return context.Users.Where(x => x.Email.StartsWith(email)).Select(x => new ProjectMember(x.UserName, x.Email, true, null, x.FirstName, x.LastName, x.AvatarUrl)).ToArray();
            }
        }

        public DatabaseInfo GetDatabaseInfo()
        {
            try
            {
                using (var context = GetDataContext())
                {
                    //TODO: Get the database version from DBVersion
                    //context.DBVersion

                    System.Diagnostics.Debug.WriteLine(context.Connection.ServerVersion + " " + context.Connection.State);
                    return new DatabaseInfo(true, context.Connection.DataSource, context.Connection.Database, -1);
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

        public IEnumerable<Entity.Application> GetApplications(string userName, Guid projectKey)
        {
            using (var context = GetDataContext())
            {
                return context.Applications.Where(x => x.Project.User.UserName == userName && x.Project.ProjectKey == projectKey).Select(x => new Entity.Application(x.ApplicationKey, x.Name)).ToArray();
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
                return context.Versions.Where(x => x.Application.Project.User.UserName == userName && x.Application.ApplicationKey == applicationKey).Select(x => new Entity.Version(x.VersionKey, x.VersionNumber)).ToArray();
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
                            x.Type.Equals(type) && 
                            x.Level.Equals(issueLevel) &&
                            x.Message.Equals(message) &&
                            (stackTrace == null ? x.StackTrace == null : x.StackTrace == stackTrace));

                return issueType?.IssueTypeKey;
            }
        }

        public void CreateIssueType(Guid issueTypeKey, Guid versionKey, int ticket, string type, string issueLevel, string message, string stackTrace, DateTime serverTime)
        {
            using (var context = GetDataContext())
            {
                var itk = GetIssueTypeKey(issueTypeKey, type, issueLevel, message, stackTrace);
                if (itk != null) throw new InvalidOperationException("A IssueType with this signature already exists.");

                var newIssueType = new IssueType
                {
                    IssueTypeKey = issueTypeKey,
                    VersionId = context.Versions.Single(x => x.VersionKey == versionKey).VersionId,
                    Ticket = ticket,
                    Type = type,
                    Level = issueLevel,
                    Message = message,
                    StackTrace = stackTrace,
                    CreationServerTime = serverTime,
                };

                context.IssueTypes.InsertOnSubmit(newIssueType);
                context.SubmitChanges();
            }
        }

        public void SetSessionEnd(string sessionToken, DateTime serverDateTime)
        {
            using (var context = GetDataContext())
            {
                var session = context.Sessions.Single(x => x.SessionToken == sessionToken);
                session.EndServerTime = serverDateTime;
                context.SubmitChanges();
            }
        }

        public void SetSessionUsed(string sessionToken, DateTime serverDateTime)
        {
            using (var context = GetDataContext())
            {
                var session = context.Sessions.Single(x => x.SessionToken == sessionToken);
                session.LastUsedServerTime = serverDateTime;
                context.SubmitChanges();
            }
        }

        public void CreateSession(string sessionToken, DateTime clientStartTime, string callerIp, Guid versionKey, Guid? applicationUserKey, Guid? machineKey, string environment, DateTime serverTime)
        {
            using (var context = GetDataContext())
            {
                var newSession = new Session
                {
                    SessionToken = sessionToken,
                    CallerIp = callerIp,
                    StartClientTime = clientStartTime,
                    StartServerTime = serverTime,
                    LastUsedServerTime = serverTime,
                    EndServerTime = null,
                    ApplicationUserId = context.ApplicationUsers.Single(x => x.ApplicationUserKey == applicationUserKey).ApplicationUserId,
                    MachineId = context.Machines.Single(x => x.MachineKey == machineKey).MachineId,
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
                var userData = context.ApplicationUsers.SingleOrDefault(x => x.Fingerprint == fingerprint && x.Project.ProjectKey == projectKey);
                return userData?.ApplicationUserKey;
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

        public void CreateIssue(Guid issueKey, Guid issueTypeKey, string sessionToken, DateTime clientTime, IDictionary<string, string> data, DateTime serverTime)
        {
            using (var context = GetDataContext())
            {
                //var machine = context.Machines.SingleOrDefault(x => x.Sessions.Single(y => y.SessionKey == sessionKey).MachineId == x.MachineId);
                //var applicationUser = context.ApplicationUsers.SingleOrDefault(x => x.Sessions.Single(y => y.SessionKey == sessionKey).ApplicationUserId == x.ApplicationUserId);

                var issue = new Issue
                {
                    IssueKey = issueKey,
                    IssueTypeId = context.IssueTypes.Single(x => x.IssueTypeKey == issueTypeKey).IssueTypeId,
                    CreationClientTime = clientTime,
                    CreationServerTime = serverTime,
                    SessionId = context.Sessions.Single(x => x.SessionToken == sessionToken).SessionId,
                    //MachineKey = machine.Id, //TODO: Allow the machineId to be null.
                    //UserDataKey = userData.Id, //TODO: Allow the userdata (IE. ApplicationUser) to be null.
                };

                context.Issues.InsertOnSubmit(issue);

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

        public Entity.Session GetSession(string sessionToken)
        {
            using (var context = GetDataContext())
            {
                var session = context.Sessions.SingleOrDefault(x => x.SessionToken == sessionToken);
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
                //var user = context.Users.SingleOrDefault(x => string.Equals(x.UserName, userName, StringComparison.CurrentCultureIgnoreCase));
                //var projectUser = context.ProjectUsers.SingleOrDefault(x => x.Project.ProjectKey == projectKey && x.UserId == user.UserId);

                ////TODO: This check should be in the business layer
                //if (projectUser == null)
                //{
                //    throw new InvalidOperationException("The user doesn't have access to the provided project.");
                //}

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

        public Entity.ProjectPageProject[] GetProjects(string userName)
        {
            using (var context = GetDataContext())
            {
                var projectPageApplicaitons = context.Projects.Where(x => x.User.UserName == userName || x.ProjectUsers.Any(y => y.User.UserName == userName && x.ProjectId == y.ProjectId));
                return projectPageApplicaitons.Select(x => new Entity.ProjectPageProject { Name = x.Name, DashboardColor = x.DashboardColor, ProjectKey = x.ProjectKey, ProjectApiKey = x.ProjectApiKey }).ToArray();
            }
        }

        private static Quilt4DataContext GetDataContext()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            return new Quilt4DataContext(connectionString);
        }
    }
}