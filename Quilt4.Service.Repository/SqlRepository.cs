﻿using System;
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
        //TODO: Persist to SQL Server
        private static readonly IDictionary<string, string> _settings = new Dictionary<string, string>();
        private static readonly IDictionary<string, LoginSession> _loginSession = new Dictionary<string, LoginSession>();

        public void SaveUser(Entity.User user)
        {
            using (var context = GetDataContext())
            {
                var dbUser = context.Users.SingleOrDefault(x => x.UserName == user.Username);
                if (dbUser == null)
                {
                    dbUser = new User { CreateTime = DateTime.UtcNow, UserKey = user.UserKey, EmailConfirmed = false };
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
                var project = context.Projects.Single(x => x.Id == projectKey);
                context.Projects.DeleteOnSubmit(project);
                context.SubmitChanges();

                //TODO: Also delete from all read-tables
            }
        }

        public void SaveLoginSession(LoginSession loginSession)
        {
            _loginSession.Add(loginSession.PublicKey, loginSession);
        }

        public T GetSetting<T>(string name)
        {
            if (!_settings.ContainsKey(name))
            {
                return default(T);
            }

            var value = _settings[name];
            var result = (T)Convert.ChangeType(value, typeof(T));
            return result;
        }

        public T GetSetting<T>(string name, T defaultValue)
        {
            if (!_settings.ContainsKey(name))
            {
                SetSetting(name, defaultValue);
            }

            var value = _settings[name];
            var result = (T)Convert.ChangeType(value, typeof(T));
            return result;
        }

        public void SetSetting<T>(string name, T value)
        {
            if (_settings.ContainsKey(name))
                _settings.Remove(name);
            _settings.Add(name, value.ToString());
        }

        public int GetNextTicket(Guid projectKey) //, string type, string message, string stackTrace, string issueLevel, Guid versionId)
        {
            using (var context = GetDataContext())
            {
                //var issueType = context.IssueTypes.SingleOrDefault(x => x.Type == type && x.Message == message && x.StackTrace == stackTrace && x.Level == issueLevel && x.VersionId == versionId);
                //if (issueType != null)
                //    return issueType.Ticket;

                int ticket;

                using (var scope = new TransactionScope())
                {
                    var project = context.Projects.Single(x => x.Id == projectKey);

                    project.LastTicket++;
                    ticket = project.LastTicket;
                    context.SubmitChanges();
                    scope.Complete();
                }

                return ticket;
            }
        }

        public Guid GetProjectKey(string projectApiKey)
        {
            using (var context = GetDataContext())
            {
                var project = context.Projects.SingleOrDefault(x => x.ClientToken == projectApiKey);

                if (project == null)
                {
                    throw new ArgumentException("There is no project with provided projectApiKey.");
                }

                return project.Id;
            }
        }

        public Guid SaveApplication(Guid projectKey, string name)
        {
            using (var context = GetDataContext())
            {
                var application = context.Applications.SingleOrDefault(x => x.ProjectId == projectKey && x.Name == name);

                if (application != null)
                {
                    //Update application?
                    return application.Id;
                }

                var newApplication = new Application
                {
                    Id = Guid.NewGuid(),
                    ProjectId = projectKey,
                    Name = name,
                    CreationDate = DateTime.UtcNow,
                    LastUpdateDate = DateTime.UtcNow
                };

                context.Applications.InsertOnSubmit(newApplication);
                context.SubmitChanges();

                return newApplication.Id;
            }
        }

        public Guid SaveVersion(Guid applicaitonKey, string version, string supportToolkitNameVersion)
        {
            using (var context = GetDataContext())
            {
                var existingVersion =
                    context.Versions.SingleOrDefault(x => x.ApplicationId == applicaitonKey && x.Version1 == version);

                if (existingVersion != null)
                {
                    //Update version?
                    return existingVersion.Id;
                }

                var newVersion = new Version
                {
                    Id = Guid.NewGuid(),
                    ApplicationId = applicaitonKey,
                    Version1 = version,
                    SupportToolkitVersion = supportToolkitNameVersion,
                    CreationDate = DateTime.UtcNow,
                    LastUpdateDate = DateTime.UtcNow
                };

                context.Versions.InsertOnSubmit(newVersion);
                context.SubmitChanges();

                return newVersion.Id;
            }
        }

        public Guid SaveIssueType(Guid versionKey, int ticket, string type, string issueLevel, string message,
                                  string stackTrace)
        {
            using (var context = GetDataContext())
            {
                var issueType =
                    context.IssueTypes.SingleOrDefault(
                        x =>
                        x.VersionId.Equals(versionKey) && x.Type.Equals(type) && x.Level.Equals(issueLevel) &&
                        x.Message.Equals(message) &&
                        (stackTrace == null ? x.StackTrace == null : x.StackTrace == stackTrace));

                if (issueType != null)
                {
                    //Update issueType?
                        
                    return issueType.Id;
                }

                var newIssueType = new IssueType
                {
                    Id = Guid.NewGuid(),
                    VersionId = versionKey,
                    Ticket = ticket,
                    Type = type,
                    Level = issueLevel,
                    Message = message,
                    StackTrace = stackTrace,
                    CreationDate = DateTime.UtcNow,
                    LastUpdateDate = DateTime.UtcNow
                };

                context.IssueTypes.InsertOnSubmit(newIssueType);
                context.SubmitChanges();

                return newIssueType.Id;
            }
            
        }

        public void SetSessionEnd(Guid sessionKey, DateTime serverDateTime)
        {
            using (var context = GetDataContext())
            {
                var session = context.Sessions.Single(x => x.Id == sessionKey);
                session.ServerEndTime = serverDateTime;
                context.SubmitChanges();
            }
        }

        public void SetSessionUsed(Guid sessionKey, DateTime serverDateTime)
        {
            //TODO: Set the server last used time
            //using (var context = GetDataContext())
            //{
            //    var session = context.Sessions.Single(x => x.Id == sessionKey);
            //    session.ServerLastUsedTime = serverDateTime;
            //    context.SubmitChanges();
            //}
        }

        public void CreateSession(Guid sessionKey, DateTime clientStartTime, string callerIp, Guid applicaitonKey, Guid versionKey, Guid? applicationUserKey, Guid? machineKey, string environment, DateTime serverTime)
        {
            using (var context = GetDataContext())
            {
                //TODO: Move this check to the business layer
                var session = context.Sessions.SingleOrDefault(x => x.Id == sessionKey);
                if (session != null)
                {
                    throw new InvalidOperationException("A session with this key has already been registered.");
                }

                var newSession = new Session
                {
                    Id = sessionKey,
                    CallerIp = callerIp,
                    ClientStartTime = clientStartTime,
                    ServerStartTime = serverTime,
                    ServerEndTime = null,
                    ApplicationId = applicaitonKey,
                    VersionId = versionKey,
                    UserDataId = applicationUserKey.Value, //TODO: We should allow null here if no user tracking is used.
                    MachineId = machineKey.Value, //TODO: We should allow null here if no machine tracking is used.
                    Enviroment = environment
                };

                context.Sessions.InsertOnSubmit(newSession);
                context.SubmitChanges();
            }
        }

        public Guid SaveApplicationUser(Guid projectKey, string fingerprint, string userName, DateTime updateTime)
        {
            //TODO: Save the application user per project. The fingerprint should be unique per project.

            using (var context = GetDataContext())
            {
                var userData = context.UserDatas.SingleOrDefault(x => x.Fingerprint == fingerprint);

                if (userData != null)
                {
                    if (userData.UserName == userName)
                        return userData.Id;

                    userData.UserName = userName;
                    userData.LastUpdateDate = updateTime;
                    context.SubmitChanges();
                    
                    return userData.Id;
                }

                var newUserData = new UserData
                {
                    Id = Guid.NewGuid(),
                    Fingerprint = fingerprint,
                    UserName = userName,
                    CreationDate = DateTime.UtcNow,
                    LastUpdateDate = DateTime.UtcNow
                };

                context.UserDatas.InsertOnSubmit(newUserData);
                context.SubmitChanges();
                
                return newUserData.Id;
            }
        }

        //TODO: Make this method a bit more clean
        public Guid SaveMachine(Guid projectKey, string fingerprint, string name, IDictionary<string, string> data)
        {
            using (var context = GetDataContext())
            {
                var machine = context.Machines.SingleOrDefault(x => x.Fingerprint == fingerprint);

                if (machine != null)
                {
                    var needToSubmitChanges = false;

                    if (machine.Name != name)
                    {
                        machine.Name = name;
                        machine.LastUpdateDate = DateTime.UtcNow;
                        needToSubmitChanges = true;
                    }

                    //To improve performance ?
                    var machineDatas = machine.MachineDatas.ToArray();

                    if (data != null)
                    {
                        //Add missing and update existing machineDatas
                        foreach (var d in data)
                        {
                            var match = machineDatas.SingleOrDefault(x => x.Name == d.Key);

                            if (match == null)
                            {
                                var newMachineData = new MachineData
                                {
                                    Id = Guid.NewGuid(),
                                    MachineId = machine.Id,
                                    CreationDate = DateTime.UtcNow,
                                    LastUpdateDate = DateTime.UtcNow,
                                    Name = d.Key,
                                    Value = d.Value
                                };

                                context.MachineDatas.InsertOnSubmit(newMachineData);

                                needToSubmitChanges = true;

                                continue;
                            }

                            if (match.Value != d.Value)
                            {
                                match.Value = d.Value;
                                match.LastUpdateDate = DateTime.UtcNow;

                                needToSubmitChanges = true;
                            }
                        }
                    }

                    //Remove old machineDatas
                    foreach (var machineData in machineDatas)
                    {
                        var match = data.SingleOrDefault(x => x.Key == machineData.Name);

                        if (match.Equals(new KeyValuePair<string, string>()))
                        {
                            context.MachineDatas.DeleteOnSubmit(machineData);
                            needToSubmitChanges = true;
                        }
                    }


                    if (needToSubmitChanges)
                        context.SubmitChanges();

                    return machine.Id;
                }

                var newMachine = new Machine
                {
                    Id = Guid.NewGuid(),
                    Fingerprint = fingerprint,
                    Name = name,
                    CreationDate = DateTime.UtcNow,
                    LastUpdateDate = DateTime.UtcNow
                };

                context.Machines.InsertOnSubmit(newMachine);

                if (data != null)
                {
                    foreach (var d in data)
                    {
                        var machineData = new MachineData
                        {
                            Id = Guid.NewGuid(),
                            MachineId = newMachine.Id,
                            CreationDate = DateTime.UtcNow,
                            LastUpdateDate = DateTime.UtcNow,
                            Name = d.Key,
                            Value = d.Value
                        };

                        context.MachineDatas.InsertOnSubmit(machineData);
                    }
                }


                context.SubmitChanges();

                return newMachine.Id;
            }
        }

        public void SaveIssue(Guid issueId, Guid issueTypeId, Guid sessionId, DateTime clientTime, IDictionary<string, string> data)
        {
            using (var context = GetDataContext())
            {
                var machine = context.Machines.Single(x => x.Sessions.Single(y => y.Id == sessionId).MachineId == x.Id);
                var userData = context.UserDatas.Single(x => x.Sessions.Single(y => y.Id == sessionId).UserDataId == x.Id);

                var issue = new Issue
                {
                    Id = issueId,
                    IssueTypeId = issueTypeId,
                    ClientCreationDate = clientTime,
                    CreationDate = DateTime.UtcNow,
                    LastUpdateDate = DateTime.UtcNow,
                    SessionId = sessionId,
                    MachineId = machine.Id,
                    UserDataId = userData.Id,
                };

                context.Issues.InsertOnSubmit(issue);

                if (data != null)
                {
                    foreach (var d in data)
                    {
                        var issueData = new IssueData
                        {
                            Id = Guid.NewGuid(),
                            IssueId = issueId,
                            Name = d.Key,
                            Value = d.Value
                        };

                        context.IssueDatas.InsertOnSubmit(issueData);
                    }
                }

                context.SubmitChanges();

                //return issue.Id;
            }
        }

        public Entity.Session GetSession(Guid sessionId)
        {
            using (var context = GetDataContext())
            {
                var session = context.Sessions.SingleOrDefault(x => x.Id == sessionId);

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
                    Id = projectKey,
                    Name = name,
                    DashboardColor = dashboardColor,
                    CreationDate = createTime,
                    LastUpdateDate = createTime,
                    ClientToken = projectApiKey,
                    OwnerUserId = user.UserId,
                    LastTicket = 0,
                };

                context.Projects.InsertOnSubmit(project);

                var projectUser = new ProjectUser
                {
                    ProjectId = projectKey,
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

                var projectUser = context.ProjectUsers.SingleOrDefault(x => x.ProjectId == projectKey && x.UserId == user.UserId);

                if (projectUser == null)
                    throw new InvalidOperationException("The user doesn't have access to the provided project.");

                var project = context.Projects.Single(x => x.Id == projectKey);

                project.Name = name;
                project.DashboardColor = dashboardColor;
                project.LastUpdateDate = updateTime;

                context.SubmitChanges();
            }
        }

        public Entity.ProjectPageProject[] GetProjects(string userId)
        {
            using (var context = GetDataContext())
            {
                var projectPageApplicaitons = context.Projects.Where(x => x.User.UserName == userId);
                return projectPageApplicaitons.Select(x => new Entity.ProjectPageProject { Name = x.Name, DashboardColor = x.DashboardColor, Id = x.Id, ClientToken = x.ClientToken }).ToArray();
            }
        }

        private static Quilt4DataContext GetDataContext()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            return new Quilt4DataContext(connectionString);
        }
    }
}