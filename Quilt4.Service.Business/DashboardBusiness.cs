using System.Collections.Generic;
using Quil4.Service.Interface.Repository;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Business
{
    public class DashboardBusiness : IDashboardBusiness
    {
        private readonly IReadRepository _readRepository;

        public DashboardBusiness(IReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public IEnumerable<DashboardPageProject> GetProjects(string userId)
        {
            return _readRepository.GetDashboardProjects(userId);
        }
    }
}