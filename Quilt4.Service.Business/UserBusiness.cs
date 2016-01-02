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

        public IEnumerable<User> GetList()
        {
            return _repository.GetUsers();
        }

        public IEnumerable<User> SearchUsers(string searchString, string callerIp)
        {
            //TODO: Set the maximum calls from the same origin within a sertain time interval (Log violations)

            var minUserSearchStringLength = _settingBusiness.GetSetting("MinUserSearchStringLength", 3);
            if (searchString.Length < minUserSearchStringLength)
                return new List<User> { };

            return _repository.GetUsers().Where(x => x.Username.StartsWith(searchString, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}