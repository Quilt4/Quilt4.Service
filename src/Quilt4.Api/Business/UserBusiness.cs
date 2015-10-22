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
            //TODO: Create a real random key here.
            var sessionKey = Guid.NewGuid().ToString();
            var loginSession = new LoginSession(sessionKey);
            _repository.SaveLoginSession(loginSession);
            return loginSession;
        }

        void IUserBusiness.CreateUser(string username, string email, string password)
        {
            var user = new User(username, email, HashPassword(password));
            _repository.SaveUser(user);
        }

        private string HashPassword(string password)
        {
            //TODO: Fix!
            return password;
        }
    }
}