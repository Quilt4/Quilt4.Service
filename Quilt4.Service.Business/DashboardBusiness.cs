using System;
using System.Collections.Generic;
using System.Linq;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business
{
    public class DashboardBusiness : IDashboardBusiness
    {
        private readonly IRepository _repository;
        private readonly IReadRepository _readRepository;

        public DashboardBusiness(IRepository repository, IReadRepository readRepository)
        {
            _repository = repository;
            _readRepository = readRepository;
        }

        public IEnumerable<DashboardPageProject> GetProjects(string userName)
        {
            throw new NotImplementedException();
            //return _readRepository.GetDashboardProjects(userName);
        }

        public IEnumerable<ProjectInvitation> GetInvitations(string userName)
        {
            return _repository.GetInvitations(userName);
        }
    }
}