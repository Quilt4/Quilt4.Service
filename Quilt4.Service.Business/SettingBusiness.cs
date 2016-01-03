using System;
using System.Collections.Generic;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;

namespace Quilt4.Service.Business
{
    public class SettingBusiness : ISettingBusiness
    {
        private readonly IRepository _repository;

        public SettingBusiness(IRepository repository)
        {
            _repository = repository;
        }

        public T GetSetting<T>(string settingName, T defaultValue)
        {
            var setting = _repository.GetSetting(settingName);

            if (setting == null)
            {
                setting = new Setting(settingName, defaultValue.ToString());
                _repository.SetSetting(setting.Name, setting.Value);
            }

            var response = (T)Convert.ChangeType(setting.Value, typeof(T));
            return response;
        }

        public IEnumerable<Setting> GetSettings()
        {
            return _repository.GetSettings();
        }
    }
}