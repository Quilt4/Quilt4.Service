using System.Collections.Generic;

namespace Quilt4.Service.Interface.Business
{
    public interface ISettingBusiness
    {
        T GetSetting<T>(string settingName, T defaultValue);
        bool HasSetting(string settingName);
        IEnumerable<Entity.Setting> GetSettings();
    }
}