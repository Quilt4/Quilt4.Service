using System;
using System.Security.Cryptography;
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

            //TODO: The task here is to generate a private-public keypair.

            //var parameters = new CspParameters();
            //parameters.
            //parameters.KeyContainerName = "MyContainer";
            //var provider = new RSACryptoServiceProvider(parameters);

            var rsaProvider = new RSACryptoServiceProvider(2048);
            var publicKey = rsaProvider.ToXmlString(false);
            var privateKey = rsaProvider.ToXmlString(true);
            
            //var blob1 = myRSA.ExportCspBlob(true);
            //var b1 = GetString(blob1);
            //var blob2 = myRSA.ExportCspBlob(false);
            //var b2 = GetString(blob2);

            //var xport = myRSA.ExportParameters(true);
            //var m = xport.Modulus;
            //var x = xport.Exponent;
            //var enc = myRSA.Encrypt(GetBytes("ABC"), false);
            //myRSA.Decrypt(enc, false);
            //myRSA.SignData()

            //var xxx = ExportPublicKeyToPEMFormat(myRSA);

            //CspParameters parameters = new CspParameters();

            //parameters.KeyContainerName = "MyContainer";
            //RSACryptoServiceProvider obj = new RSACryptoServiceProvider(parameters);
            //byte[] a = Generic.RSAEncrypt(ByteConverter.GetBytes(s[0]),
            //                              obj.ExportParameters(false), false);

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