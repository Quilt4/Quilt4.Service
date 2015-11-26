using System;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Repository.MemoryRepository
{
    public class MemoryWriteRepository : IWriteRepository
    {
        public void UpdateDashboardPageProject(Guid projectId)
        {
            throw new NotImplementedException();
        }

        public void UpdateProjectPageProject(Guid projectId)
        {
            throw new NotImplementedException();
        }
        
        public void WriteToReadDb()
        {
            throw new NotImplementedException();
        }
    }
}