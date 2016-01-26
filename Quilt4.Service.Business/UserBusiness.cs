using System;
using System.Collections.Generic;
using System.Linq;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IRepository _repository;
        private readonly ISettingBusiness _settingBusiness;

        public UserBusiness(IRepository repository, ISettingBusiness settingBusiness)
        {
            _repository = repository;
            _settingBusiness = settingBusiness;
        }

        public UserInfo GetUser(string userName)
        {
            var x = _repository.GetUserInfo(userName);
            var userInfo = new UserInfo(x.UserKey, x.Username, x.Email, x.FirstName, x.LastName, x.Email.GetGravatarPath());
            return userInfo;
        }

        public IEnumerable<UserInfo> GetList()
        {
            return _repository
                .GetUsers()
                .Select(x => new UserInfo(x.UserKey, x.Username, x.Email,x.FirstName,x.LastName, x.Email.GetGravatarPath()))
                .ToArray();
        }

        public IEnumerable<UserInfo> SearchUsers(string searchString, string callerIp)
        {
            //TODO: Set the maximum calls from the same origin within a certain time interval (Log violations)

            var minUserSearchStringLength = _settingBusiness.GetSetting("UserSearchStringMinLength", 1);
            if (searchString.Length < minUserSearchStringLength)
                return new List<UserInfo>();

            return _repository
                .GetUsers()
                .Where(x => x.Username.StartsWith(searchString, StringComparison.InvariantCultureIgnoreCase) || x.Email.StartsWith(searchString, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => new UserInfo(x.UserKey, x.Username, x.Email, x.FirstName, x.LastName, x.Email.GetGravatarPath()))
                .ToArray();
        }
    }
}