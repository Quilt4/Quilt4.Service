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
        private readonly IEmailSender _emailSender;

        public InvitationBusiness(IRepository repository, ISettingBusiness settingBusiness, IEmailSender emailSender)
        {
            _repository = repository;
            _settingBusiness = settingBusiness;
            _emailSender = emailSender;
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

            var projectInvitations = _repository.GetInvitations().Where(x => x.ProjectKey == projectKey);
            if (projectInvitations.Any(x => user == x.UserEMail || (userEntity != null && userEntity.Username == x.UserName)))
                throw new InvalidOperationException("There is already an invitation to this proect for the provided user.");

            string userKey = null;
            string email = null;
            if (userEntity != null)
            {
                userKey = userEntity.UserKey;
            }
            else
            {
                //TODO: Set a limit to email invitations that can be made by a single user, and how frequent it can be.
                //How frequent can a regular user invite be?

                //TODO: Move the content somewhere so that it can be changed.
                //TODO: Make the accept and decline links work.

                _emailSender.Send(user, "Invitation to Quilt4", string.Format("User {0} want to invite you to the project {1}. Click Accept or Decline.", userName, "???"));
                email = user;
            }            

            var inviteCode = RandomUtility.GetRandomString(12);

            _repository.CreateProjectInvitation(projectKey, userName, inviteCode, userKey, email, DateTime.UtcNow);
        }

        public void Accept(string userName, string inviteCode)
        {
            //TODO: Create a transaction scobe around theese lines.
            var invitation = _repository.GetInvitations(userName).Single(x => x.InviteCode == inviteCode);
            _repository.AddProjectMember(userName, invitation.ProjectKey, "User");
            _repository.DeleteProjectInvitation(invitation.ProjectKey, userName);
        }
    }
}