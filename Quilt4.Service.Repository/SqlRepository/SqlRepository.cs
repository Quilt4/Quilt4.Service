using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Transactions;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Repository;
using Quilt4.Service.Repository.SqlRepository.Extensions;

namespace Quilt4.Service.Repository.SqlRepository
{
    //TODO: Persist to SQL Server
    public class SqlRepository : IRepository
    {
        private static readonly IDictionary<string, string> _settings = new Dictionary<string, string>();
        private static readonly IDictionary<string, User> _users = new Dictionary<string, User>();
        private static readonly IDictionary<string, LoginSession> _loginSession = new Dictionary<string, LoginSession>();

        public void SaveUser(User user)
        {
            _users.Add(user.Username, user);
        }

        public User GetUser(string username)
        {
            if (!_users.ContainsKey(username))
                return null;
            return _users[username];
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

        public int GetNextTicket(string clientToken, string type, string message, string stackTrace, string issueLevel, Guid versionId)
                                 string level, string message, string stackTrace)
        {
            using (var context = GetDataContext())
            {
                var issueType = context.IssueTypes.SingleOrDefault(x => x.Type == type && x.Message == message && x.StackTrace == stackTrace && x.Level == issueLevel && x.VersionId == versionId);

                    x.Type == type && x.Level == level && x.Message == message && x.StackTrace == stackTrace);
                if (issueType != null)
                    return issueType.Ticket;

                int ticket;

                using (var scope = new TransactionScope())
                {
                    var project = context.Projects.Single(x => x.ClientToken == clientToken);

                    project.LastTicket++;
                    ticket = project.LastTicket;
                    context.SubmitChanges();
                    scope.Complete();
                }

                return ticket;
            }
    }

        public Guid? GetProjectId(string clientToken)
        {
            using (var context = GetDataContext())
            {
                var project = context.Projects.SingleOrDefault(x => x.ClientToken == clientToken);

                return project?.Id;
            }
        }

        public Guid SaveApplication(Guid projectId, string name)
        {
            using (var context = GetDataContext())
            {
                var application = context.Applications.SingleOrDefault(x => x.ProjectId == projectId && x.Name == name);

                if (application != null)
                {
                    //Update application?
                    return application.Id;
                }

                var newApplication = new Application
                {
                    Id = Guid.NewGuid(),
                    ProjectId = projectId,
                    Name = name,
                    CreationDate = DateTime.Now,
                    LastUpdateDate = DateTime.Now
                };

                context.Applications.InsertOnSubmit(newApplication);
                context.SubmitChanges();

                return newApplication.Id;
            }
        }

        public Guid SaveVersion(Guid applicaitonId, string version, string supportToolkitNameVersion)
        {
            using (var context = GetDataContext())
            {
                var existingVersion =
                    context.Versions.SingleOrDefault(x => x.ApplicationId == applicaitonId && x.Version1 == version);

                if (existingVersion != null)
                {
                    //Update version?
                    return existingVersion.Id;
                }

                var newVersion = new Version
                {
                    Id = Guid.NewGuid(),
                    ApplicationId = applicaitonId,
                    Version1 = version,
                    SupportToolkitVersion = supportToolkitNameVersion,
                    CreationDate = DateTime.Now,
                    LastUpdateDate = DateTime.Now
                };

                context.Versions.InsertOnSubmit(newVersion);
                context.SubmitChanges();

                return newVersion.Id;
            }
        }

        public Guid SaveIssueType(Guid versionId, int ticket, string type, string issueLevel, string message,
                                  string stackTrace)
        {
            using (var context = GetDataContext())
            {
                var issueType =
                    context.IssueTypes.SingleOrDefault(
                        x =>
                        x.VersionId.Equals(versionId) && x.Type.Equals(type) && x.Level.Equals(issueLevel) &&
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
                    VersionId = versionId,
                    Ticket = ticket,
                    Type = type,
                    Level = issueLevel,
                    Message = message,
                    StackTrace = stackTrace,
                    CreationDate = DateTime.Now,
                    LastUpdateDate = DateTime.Now
                };

                context.IssueTypes.InsertOnSubmit(newIssueType);
                context.SubmitChanges();

                return newIssueType.Id;
            }
            
        }

        public Guid SaveSession(Guid sessionId, DateTime clientStartTime, string callerIp, Guid applicaitonId, Guid versionId, Guid userDataId, Guid machineId, string environment)
        {
            using (var context = GetDataContext())
            {
                var session = context.Sessions.SingleOrDefault(x => x.Id == sessionId);

                if (session != null)
                {
                    session.ClientEndTime = DateTime.Now;
                    session.ServerEndTime = DateTime.Now;
                    context.SubmitChanges();

                    return session.Id;
                }

                var newSession = new Session
                {
                    Id = sessionId,
                    CallerIp = callerIp,
                    ClientStartTime = clientStartTime,
                    ServerStartTime = DateTime.Now,
                    ServerEndTime = DateTime.Now,
                    ClientEndTime = clientStartTime,
                    ApplicationId = applicaitonId,
                    VersionId = versionId,
                    UserDataId = userDataId,
                    MachineId = machineId,
                    Enviroment = environment
                };

                context.Sessions.InsertOnSubmit(newSession);
                context.SubmitChanges();

                return newSession.Id;
            }
        }

        public Guid SaveUserData(string fingerprint, string userName)
        {
            using (var context = GetDataContext())
            {
                var userData = context.UserDatas.SingleOrDefault(x => x.Fingerprint == fingerprint);

                if (userData != null)
                {
                    if (userData.UserName == userName)
                        return userData.Id;

                    userData.UserName = userName;
                    userData.LastUpdateDate = DateTime.Now;
                    context.SubmitChanges();
                    
                    return userData.Id;
                }

                var newUserData = new UserData
                {
                    Id = Guid.NewGuid(),
                    Fingerprint = fingerprint,
                    UserName = userName,
                    CreationDate = DateTime.Now,
                    LastUpdateDate = DateTime.Now
                };

                context.UserDatas.InsertOnSubmit(newUserData);
                context.SubmitChanges();
                
                return newUserData.Id;
            }
        }

        //TODO: Make this method a bit more clean
        public Guid SaveMachine(string fingerprint, string name, IDictionary<string, string> data)
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
                        machine.LastUpdateDate = DateTime.Now;
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
                                    CreationDate = DateTime.Now,
                                    LastUpdateDate = DateTime.Now,
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
                                match.LastUpdateDate = DateTime.Now;

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
                    CreationDate = DateTime.Now,
                    LastUpdateDate = DateTime.Now
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
                            CreationDate = DateTime.Now,
                            LastUpdateDate = DateTime.Now,
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

        public Guid SaveIssue(Guid issueId, Guid issueTypeId, Guid sessionId,
                              DateTime clientTime, IDictionary<string, string> data)
        {
            using (var context = GetDataContext())
            {
                var issue = new Issue
                {
                    Id = issueId,
                    IssueTypeId = issueTypeId,
                    ClientCreationDate = clientTime,
                    CreationDate = DateTime.Now,
                    LastUpdateDate = DateTime.Now,
                    SessionId = sessionId,
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

                return issue.Id;
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

        public void CreateProject(Guid projectKey, string name, string dashboardColor)
        {
            using (var context = GetDataContext())
            {
                var project = new Project
                {
                    Id = projectKey,
                    Name = name,
                    DashboardColor = dashboardColor,
                    CreationDate = DateTime.Now,
                    LastUpdateDate = DateTime.Now,
                    ClientToken = Guid.NewGuid().ToString().Replace("-", "")
                };

                context.Projects.InsertOnSubmit(project);
                context.SubmitChanges();
            }
        }

        public void UpdateProject(Guid projectId, string name, string dashboardColor)
        {
            using (var context = GetDataContext())
            {
                var project = context.Projects.Single(x => x.Id == projectId);

                project.Name = name;
                project.DashboardColor = dashboardColor;
                project.LastUpdateDate = DateTime.Now;

                context.SubmitChanges();
            }
        }

        private static Quilt4DataContext GetDataContext()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            return new Quilt4DataContext(connectionString);
        }
    }
}