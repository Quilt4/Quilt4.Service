//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Quilt4.Service.Entity;
//using Quilt4.Service.Interface.Business;
//using Quilt4.Service.Interface.Repository;
//using Quilt4.Service.MemoryRepository;

//namespace Quilt4.Service.Business
//{
//    public class DashboardBusiness : IDashboardBusiness
//    {
//        private readonly IProjectRepository _projectRepository;

//        public DashboardBusiness(IProjectRepository projectRepository)
//        {
//            _projectRepository = projectRepository;
//        }

//        public IEnumerable<DashboardPageProject> GetProjects(string userName)
//        {
//            throw new NotImplementedException();
//            //return _readRepository.GetDashboardProjects(userName);
//        }

//        public IEnumerable<ProjectInvitation> GetInvitations(string userName)
//        {
//            return _projectRepository.GetInvitations(userName);
//        }
//    }
//}