using System;
using System.Linq;
using Quilt4.Service.Interface.Repository;
using Quilt4.Service.SqlRepository.Data;

namespace Quilt4.Service.SqlRepository.Read
{
    public class UpdateReadRepository : IUpdateReadRepository
    {
        private readonly UpdateReadRepositoryContext _updateReadRepositoryContext;
        private readonly DataRepositoryContext _dataRepositoryContext;

        public UpdateReadRepository(UpdateReadRepositoryContext updateReadRepositoryContext, DataRepositoryContext dataRepositoryContext)
        {
            _updateReadRepositoryContext = updateReadRepositoryContext;
            _dataRepositoryContext = dataRepositoryContext;
        }

        public void CreateUser(string userName)
        {
            _updateReadRepositoryContext.Execute(context =>
            {
                context.Users.InsertOnSubmit(new User { UserName = userName });
                context.SubmitChanges();
            });
        }

        public void UpdateDashboardPageProject(Guid projectKey)
        {
            _dataRepositoryContext.Execute(dataContext =>
            {
                var project = dataContext.Projects.Single(x => x.ProjectKey == projectKey);
                var ownerUserName = project.User.UserName;

                _updateReadRepositoryContext.Execute(context =>
                {
                    var dashboardProject = context.DashboardPageProjects.SingleOrDefault(x => x.ProjectKey == projectKey);

                    if (dashboardProject == null)
                    {
                        dashboardProject = new DashboardPageProject
                        {
                            ProjectKey = projectKey,
                        };

                        context.DashboardPageProjects.InsertOnSubmit(dashboardProject);
                    }
                    else
                    {
                        context.UserProjects.DeleteAllOnSubmit(context.UserProjects.Where(x => x.ProjectId == dashboardProject.ProjectId));
                    }

                    var versionCount = project.Applications.SelectMany(x => x.Versions).Count();
                    var sessionCount = project.Applications.SelectMany(x => x.Sessions).Count();
                    var issueTypeCount = project.Applications.SelectMany(x => x.Versions).SelectMany(y => y.IssueTypes).Count();
                    var issueCount = project.Applications.SelectMany(x => x.Versions).SelectMany(y => y.IssueTypes).SelectMany(z => z.Issues).Count();

                    dashboardProject.Name = project.Name;
                    dashboardProject.VersionCount = versionCount;
                    dashboardProject.SessionCount = sessionCount;
                    dashboardProject.IssueTypeCount = issueTypeCount;
                    dashboardProject.IssueCount = issueCount;
                    dashboardProject.DashboardColor = project.DashboardColor;

                    context.SubmitChanges();

                    var ownerUserId = context.Users.Single(x => x.UserName == ownerUserName).UserId;
                    context.UserProjects.InsertOnSubmit(new UserProject { ProjectId = dashboardProject.ProjectId, UserId = ownerUserId });

                    context.SubmitChanges();
                });
            });
        }

        public void UpdateProjectPageProject(Guid projectKey)
        {
            _dataRepositoryContext.Execute(dataContext =>
            {
                _updateReadRepositoryContext.Execute(context =>
                {
                    var project = dataContext.Projects.Single(x => x.ProjectKey == projectKey);

                    var dashboardPageProject = context.DashboardPageProjects.Single(x => x.ProjectKey == projectKey);
                    var projectPageProject = context.ProjectPageProjects.SingleOrDefault(x => x.ProjectKey == projectKey);

                    if (projectPageProject == null)
                    {
                        projectPageProject = new ProjectPageProject
                        {
                            ProjectKey = projectKey,
                        };

                        context.ProjectPageProjects.InsertOnSubmit(projectPageProject);
                    }

                    projectPageProject.ProjectId = dashboardPageProject.ProjectId;
                    projectPageProject.Name = project.Name;
                    projectPageProject.DashboardColor = project.DashboardColor;
                    projectPageProject.ProjectApiKey = project.ProjectApiKey;

                    context.SubmitChanges();
                });
            });
        }

        public void UpdateProjectPageApplication(Guid projectKey, Guid applicaitonKey)
        {
            _dataRepositoryContext.Execute(dataContext =>
            {
                _updateReadRepositoryContext.Execute(context =>
                {
                    //                var application = context.Applications.Single(x => x.Id == applicaitonId && x.ProjectId == projectId);

                    //                var versions = application.Versions.Count;

                    //                var projectPageApplication =
                    //                    context.ProjectPageApplications.SingleOrDefault(
                    //                        x => x.ProjectId == projectId && x.Id == application.Id);

                    //                if (projectPageApplication != null)
                    //                {
                    //                    projectPageApplication.Name = application.Name;
                    //                    projectPageApplication.Versions = versions;
                    //                }
                    //                else
                    //                {
                    //                    var newProjectPageApplication = new ProjectPageApplication
                    //                    {
                    //                        Id = application.Id,
                    //                        ProjectId = application.ProjectId,
                    //                        Name = application.Name,
                    //                        Versions = versions
                    //                    };

                    //                    context.ProjectPageApplications.InsertOnSubmit(newProjectPageApplication);
                    //                }

                    //                context.SubmitChanges();
                    throw new NotImplementedException();
                });
            });
        }

        public void UpdateProjectPageVersion(Guid projectKey, Guid applicaitonKey, Guid versionKey)
        {
            throw new NotImplementedException();
        }

        public void UpdateVersionPageVersion(Guid projectKey, Guid applicaitonKey, Guid versionKey)
        {
            throw new NotImplementedException();
        }
    }
}