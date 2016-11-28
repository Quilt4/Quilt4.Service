using System;
using System.Collections.Generic;
using System.Linq;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business
{
    public class UserAccessBusiness : IUserAccessBusiness
    {
        private readonly IReadRepository _readRepository;

        public UserAccessBusiness(IReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public void AssureAccess(string userName, Guid projectKey)
        {
            throw new NotImplementedException();
            //var projectUsers = _readRepository.GetProjectUsers(projectKey).ToArray();
            //if (projectUsers.All(x => x != userName)) throw new InvalidOperationException("The user doesn't have access to the provided project.");
        }

        public void AssureAccess(string userName, IEnumerable<Guid> projectKeys)
        {
            foreach (var projectKey in projectKeys)
            {
                AssureAccess(userName, projectKey);
            }
        }
    }
}