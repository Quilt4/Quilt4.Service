using System;
using System.Collections.Generic;
using System.Linq;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business
{
    public class InvitationBusiness : IInvitationBusiness
    {
        private readonly IRepository _repository;
        private readonly ISettingBusiness _settingBusiness;

        public InvitationBusiness(IRepository repository, ISettingBusiness settingBusiness)
        {
            _repository = repository;
            _settingBusiness = settingBusiness;
        }

        public IEnumerable<ProjectInvitation> GetUserInvitations(string userName)
        {
            return _repository.GetInvitations(userName);
        }

        public void Invite(string userName, Guid projectKey, string user)
        {
            if (_repository.GetProjects(userName).All(x => x.ProjectKey != projectKey))
                throw new InvalidOperationException("The user doesn't have access to the provided project.");

            var userEntity = _repository.GetUserByUserName(user) ?? _repository.GetUserByEMail(user);

            string userKey = null;
            string email = null;
            if (userEntity != null)
                userKey = userEntity.UserKey;
            else
                email = user;

            var inviteCode = RandomUtility.GetRandomString(12);

            _repository.CreateProjectInvitation(projectKey, userName, inviteCode, userKey, email, DateTime.UtcNow);
        }

        public void Accept(string userName, string inviteCode)
        {
            throw new NotImplementedException();
        }
    }
}