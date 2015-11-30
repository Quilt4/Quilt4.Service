﻿using System;
using System.Security.Cryptography;
using Quil4.Service.Interface.Business;
using Quil4.Service.Interface.Repository;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly ISettingBusiness _settingBusiness;
        private readonly IRepository _repository;

        public UserBusiness(ISettingBusiness settingBusiness, IRepository repository)
        {
            _settingBusiness = settingBusiness;
            _repository = repository;
        }

        public LoginSession Login(string username, string password, string publicKey)
        {
            var user = _repository.GetUser(username);

            if (user.PasswordHash != password.ToMd5Hash(_settingBusiness.GetPasswordPadding()))
                throw new InvalidOperationException("Provided password is invalid.");

            string privateKey = null;
            if (string.IsNullOrEmpty(publicKey))
            {
                var rsaProvider = new RSACryptoServiceProvider(512);
                publicKey = rsaProvider.ToXmlString(false);
                privateKey = rsaProvider.ToXmlString(true);

                //var rp = RSA.Create();
                //rp.FromXmlString(publicKey);
            }

            var loginSession = new LoginSession(publicKey, privateKey);
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