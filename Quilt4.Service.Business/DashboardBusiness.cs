using System;
using System.Collections.Generic;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business
{
    public class DashboardBusiness : IDashboardBusiness
    {
        private readonly IReadRepository _readRepository;

        public DashboardBusiness(IReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public IEnumerable<IDashboardPageProject> GetProjects(string userId)
        {
            throw new NotImplementedException();
            //return _readRepository.GetDashboardProjects(userId);
        }
    }
}