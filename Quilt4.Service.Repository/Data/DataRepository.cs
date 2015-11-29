using System;
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
    }
}