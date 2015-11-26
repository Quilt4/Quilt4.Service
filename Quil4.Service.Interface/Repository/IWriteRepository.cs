using System;

namespace Quilt4.Service.Interface.Repository
{
    public interface IWriteRepository
    {
        void UpdateDashboardPageProject(Guid projectId);
        void UpdateProjectPageProject(Guid projectId);
        void WriteToReadDb();
    }
}