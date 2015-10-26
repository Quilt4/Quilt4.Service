using System;
using Quilt4.Api.Entities;
using Quilt4.Api.Interfaces;

namespace Quilt4.Api.Business
{
    internal class UserBusiness : IUserBusiness
    {
        private readonly IRepository _repository;

        public UserBusiness(IRepository repository)
        {
            _repository = repository;
        }

        public LoginSession Login(string username, string password)
        {
            var user = _repository.GetUser(username);

            if ( user.PasswordHash != password.ToMd5Hash())
                throw new InvalidOperationException();

            var sessionKey = RandomUtility.GetRandomString(32);
            var loginSession = new LoginSession(sessionKey);
            //TODO: Generate a secret that can be used for encryption
            _repository.SaveLoginSession(loginSession);
            return loginSession;
        }

        void IUserBusiness.CreateUser(string username, string email, string password)
        {
            var user = new User(username, email, (password + _repository.GetPasswordPadding()).ToMd5Hash());
            _repository.SaveUser(user);
        }        
    }
}