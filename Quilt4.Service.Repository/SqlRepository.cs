using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Transactions;
using Quilt4.Service.Interface.Repository;
using Quilt4.Service.SqlRepository.Extensions;

namespace Quilt4.Service.SqlRepository
{    
    public class SqlRepository : IRepository
    {
        public void SaveUser(Entity.User user)
        {
            using (var context = GetDataContext())
            {
                var dbUser = context.Users.SingleOrDefault(x => x.UserName == user.Username);
                if (dbUser == null)
                {
                    dbUser = new User { CreateServerTime = DateTime.UtcNow, UserKey = user.UserKey, EmailConfirmed = false };
                    context.Users.InsertOnSubmit(dbUser);
                }

                dbUser.UserName = user.Username;
                dbUser.Email = user.Email;
                dbUser.PasswordHash = user.PasswordHash;

                context.SubmitChanges();
            }
        }

        public Entity.User GetUser(string username)
        {
            using (var context = GetDataContext())
            {
                var dbUser = context.Users.SingleOrDefault(x => x.UserName == username);
                return dbUser == null ? null : new Entity.User(dbUser.UserKey, dbUser.UserName, dbUser.Email, dbUser.PasswordHash);
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
                    CreationServerDate = serverTime,
                    BuildTime = buildTime,
                };

                context.Versions.InsertOnSubmit(newVersion);
                context.SubmitChanges();
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
                    CreationServerDate = serverTime,                    
                };

                context.IssueTypes.InsertOnSubmit(newIssueType);
                context.SubmitChanges();
            }
        }

        public void SetSessionEnd(Guid sessionKey, DateTime serverDateTime)
        {
            using (var context = GetDataContext())
            {
                var session = context.Sessions.Single(x => x.SessionKey == sessionKey);
                session.EndServerTime = serverDateTime;
                context.SubmitChanges();
            }
        }

        public void SetSessionUsed(Guid sessionKey, DateTime serverDateTime)
        {
            using (var context = GetDataContext())
            {
                var session = context.Sessions.Single(x => x.SessionKey == sessionKey);
                session.LastUsedServerTime = serverDateTime;
                context.SubmitChanges();
            }
        }

        public void CreateSession(Guid sessionKey, DateTime clientStartTime, string callerIp, Guid applicaitonKey, Guid versionKey, Guid? applicationUserKey, Guid? machineKey, string environment, DateTime serverTime)
        {
            using (var context = GetDataContext())
            {
                //TODO: Move this check to the business layer
                var session = context.Sessions.SingleOrDefault(x => x.SessionKey == sessionKey);
                if (session != null)
                {
                    throw new InvalidOperationException("A session with this key has already been registered.");
                }

                var newSession = new Session
                {
                    SessionKey = sessionKey,
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

        public void CreateIssue(Guid issueKey, Guid issueTypeKey, Guid sessionKey, DateTime clientTime, IDictionary<string, string> data, DateTime serverTime)
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
                    SessionId = context.Sessions.Single(x => x.SessionKey == sessionKey).SessionId,
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

        public Entity.Session GetSession(Guid sessionId)
        {
            using (var context = GetDataContext())
            {
                var session = context.Sessions.SingleOrDefault(x => x.SessionKey == sessionId);
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
                    UserId = user.UserId
                };

                context.ProjectUsers.InsertOnSubmit(projectUser);
                context.SubmitChanges();
            }
        }

        public void UpdateProject(Guid projectKey, string name, string dashboardColor, DateTime updateTime, string userName)
        {
            using (var context = GetDataContext())
            {
                var user = context.Users.SingleOrDefault(x => string.Equals(x.UserName, userName, StringComparison.CurrentCultureIgnoreCase));
                var projectUser = context.ProjectUsers.SingleOrDefault(x => x.Project.ProjectKey == projectKey && x.UserId == user.UserId);

                //TODO: This check should be in the business layer
                if (projectUser == null)
                {
                    throw new InvalidOperationException("The user doesn't have access to the provided project.");
                }

                var project = context.Projects.Single(x => x.ProjectKey == projectKey);

                project.Name = name;
                project.DashboardColor = dashboardColor;

                context.SubmitChanges();
            }
        }

        public Entity.ProjectPageProject[] GetProjects(string userId)
        {
            using (var context = GetDataContext())
            {
                var projectPageApplicaitons = context.Projects.Where(x => x.User.UserName == userId);
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