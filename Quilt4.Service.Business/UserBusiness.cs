using System;
using System.Collections.Generic;
using System.Linq;
using Quilt4.Service.Business.Converters;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository _repository;
        private readonly ISettingBusiness _settingBusiness;

        public UserBusiness(IUserRepository repository, ISettingBusiness settingBusiness)
        {
            _repository = repository;
            _settingBusiness = settingBusiness;
        }

        public UserInfo GetUser(string userName)
        {
            var result = _repository.GetUserInfo(userName).ToUserInfo();
            return result;
        }

        public IEnumerable<UserInfo> GetAllUsers()
        {
            var result = _repository.GetUsers().Select(x => x.ToUserInfo()).ToArray();
            return result;
        }

        public IEnumerable<UserInfo> SearchUsers(string searchString, string callerIp)
        {
            //TODO: Set the maximum calls from the same origin within a certain time interval (Log violations)

            var minUserSearchStringLength = _settingBusiness.GetSetting("UserSearchStringMinLength", 1);
            if (searchString.Length < minUserSearchStringLength)
                return new List<UserInfo>();

            var result = _repository
                .GetUsers()
                .Where(x => x.Username.StartsWith(searchString, StringComparison.InvariantCultureIgnoreCase) || x.Email.StartsWith(searchString, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => x.ToUserInfo())
                .ToArray();

            return result;
        }
    }
}