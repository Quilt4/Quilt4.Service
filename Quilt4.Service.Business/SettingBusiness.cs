using System;
using System.Collections.Generic;
using Quilt4.Service.Entity;
using Quilt4.Service.Interface.Business;
using Quilt4.Service.Interface.Repository;
using Quilt4Net;

namespace Quilt4.Service.Business
{
    public class SettingBusiness : ISettingBusiness
    {
        private readonly IRepository _repository;

        public SettingBusiness(IRepository repository)
        {
            _repository = repository;
        }

        public T GetSetting<T>(string settingName)
        {
            var setting = _repository.GetSetting(settingName);
            if (setting == null) throw new InvalidOperationException("There is no setting with the provided name.").AddData("SettingName", settingName);
            var response = (T)Convert.ChangeType(setting.Value, typeof(T));
            return response;
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

        public bool HasSetting(string settingName, bool createPlaceholder)
        {
            var setting = _repository.GetSetting(settingName);
            if (setting == null)
            {
                if (createPlaceholder)
                    _repository.SetSetting(settingName, string.Empty);
                return false;
            }

            return !string.IsNullOrEmpty(setting.Value);
        }

        public IEnumerable<Setting> GetSettings()
        {
            return _repository.GetSettings();
        }

        public void SetSetting<T>(string settingName, T value)
        {
            _repository.SetSetting(settingName, value.ToString());
        }

        public void DeleteSetting(string settingName)
        {
            _repository.DeleteSettng(settingName);
        }

        public string WebUrl
        {
            get { return GetSetting<string>("WebUrl"); }
            set { SetSetting("WebUrl", value); }
        }
    }
}