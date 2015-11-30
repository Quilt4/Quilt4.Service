using System;
using System.Collections.Generic;
using System.Linq;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.SqlRepository.Data
{
    public class DataRepository : IDataRepository
    {
        private readonly DataRepositoryContext _dataRepositoryContext;

        public DataRepository(DataRepositoryContext dataRepositoryContext)
        {
            _dataRepositoryContext = dataRepositoryContext;
        }

        public void CreateUser(string userName)
        {
            _dataRepositoryContext.Execute(context =>
            {
                context.Users.InsertOnSubmit(new User { UserName = userName });
                context.SubmitChanges();
            });
        }

        public void CreateProject(string userName, Guid projectKey, string name, string projectApiKey, DateTime createDate, string dashboardColor)
        {
            _dataRepositoryContext.Execute(context =>
            {
                var project = new Project
                {
                    ProjectKey = projectKey,
                    Name = name,
                    DashboardColor = dashboardColor,
                    CreationDate = createDate,
                    LastUpdateDate = createDate,
                    ProjectApiKey = projectApiKey,
                    UserId = context.Users.Single(x => x.UserName == userName).UserId
                };

                context.Projects.InsertOnSubmit(project);
                context.SubmitChanges();
            });
        }

        public void UpdateProject(string userName, Guid projectKey, string name, DateTime lastUpdateDate, string dashboardColor)
        {
            _dataRepositoryContext.Execute(context =>
            {
                var project = context.Projects.Single(x => x.ProjectKey == projectKey);

                var userId = context.Users.Single(x => x.UserName == userName).UserId;
                if (project.User.UserId != userId) throw new InvalidOperationException("The user does not have access to the project.");

                project.Name = name;
                project.DashboardColor = dashboardColor;
                project.LastUpdateDate = lastUpdateDate;

                context.SubmitChanges();
            });
        }

        public Guid? GetProjectKeyByProjectApiKey(string projectApiKey)
        {
            var response = _dataRepositoryContext.Execute(context =>
            {
                var project = context.Projects.SingleOrDefault(x => x.ProjectApiKey == projectApiKey);
                return project?.ProjectKey;
            });
            return response;
        }

        public void SaveApplication(Guid applicationKey, Guid projectKey, string name)
        {
            throw new NotImplementedException();
        }

        public void SaveVersion(Guid versionKey, Guid applicaitonKey, string version, string supportToolkitNameVersion)
        {
            throw new NotImplementedException();
        }

        public void SaveUserData(Guid userDataKey, string fingerprint, string userName)
        {
            throw new NotImplementedException();
        }

        public void SaveMachine(Guid machineKey, string fingerprint, string name, IDictionary<string, string> data)
        {
            throw new NotImplementedException();
        }

        public void SaveSession(Guid sessionKey, DateTime clientStartTime, string callerIp, Guid applicaitonKey, Guid versionKey, Guid userDataKey, Guid machineKey, string environment)
        {
            throw new NotImplementedException();
        }
    }
}