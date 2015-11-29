using System;
using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Controllers.Project.DataTransfer
{
    internal class UpdateProjectCommandInput : IUpdateProjectCommandInput
    {
        public UpdateProjectCommandInput(string userName, Guid projectKey, string projectName, string dashboardColor)
        {
            UserName = userName;
            ProjectKey = projectKey;
            ProjectName = projectName;
            DashboardColor = dashboardColor;
        }

        public string UserName { get; }
        public Guid ProjectKey { get; }
        public string ProjectName { get; }
        public string DashboardColor { get; }
    }
}