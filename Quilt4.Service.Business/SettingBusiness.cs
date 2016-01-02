using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business
{
    public class SettingBusiness : ISettingBusiness
    {
        private static readonly object _syncRoot = new object();
        private readonly IRepository _repository;

        public SettingBusiness(IRepository repository)
        {
            _repository = repository;
        }

        public T GetSetting<T>(string settingName, T defaultValue)
        {
            //TODO: Read from the setting table
            return defaultValue;
        }
    }
}