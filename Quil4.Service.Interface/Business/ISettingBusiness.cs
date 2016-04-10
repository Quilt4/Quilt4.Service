using System.Collections.Generic;

namespace Quilt4.Service.Interface.Business
{
    public interface ISettingBusiness
    {
        T GetSetting<T>(string settingName);
        T GetSetting<T>(string settingName, T defaultValue);
        bool HasSetting(string settingName, bool createPlaceholder = false);
        IEnumerable<Entity.Setting> GetSettings();
        void SetSetting<T>(string settingName, T value);
        void DeleteSetting(string settingName);

        //TODO: This is for getting specific settings. Perhaps move to another class
        string WebUrl { get; set; }
    }
}