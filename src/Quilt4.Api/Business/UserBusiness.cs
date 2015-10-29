using System;
using Quilt4.Api.Entities;
using Quilt4.Api.Interfaces;

namespace Quilt4.Api.Business
{
    internal class UserBusiness : IUserBusiness
    {
        private readonly ISettingBusiness _settingBusiness;
        private readonly IRepository _repository;

        public UserBusiness(ISettingBusiness settingBusiness, IRepository repository)
        {
            _settingBusiness = settingBusiness;
            _repository = repository;
        }

        public LoginSession Login(string username, string password)
        {
            var user = _repository.GetUser(username);

            if ( user.PasswordHash != password.ToMd5Hash(_settingBusiness.GetPasswordPadding()))
                throw new InvalidOperationException("Provided password is invalid.");

            var sessionKey = RandomUtility.GetRandomString(32);
            var sharedSecret = RandomUtility.GetRandomString(64); //TODO: Can I crate a public/private encryption key-pair here?
            var loginSession = new LoginSession(sessionKey, sharedSecret);
            //TODO: Generate a secret that can be used for encryption
            _repository.SaveLoginSession(loginSession);
            return loginSession;
        }

        void IUserBusiness.CreateUser(string username, string email, string password)
        {
            var user = new User(username, email, password.ToMd5Hash(_settingBusiness.GetPasswordPadding()));
            _repository.SaveUser(user);
        }        
    }
}