using System;

namespace Quilt4.Service.Interface.Repository
{
    public interface IWriteRepository
    {
        void UpdateDashboardPageProject(Guid projectKey);
        void UpdateProjectPageProject(Guid projectKey);
        void WriteToReadDb();
    }
}