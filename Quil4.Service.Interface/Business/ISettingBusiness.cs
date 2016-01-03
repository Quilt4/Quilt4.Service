using System.Collections.Generic;

namespace Quilt4.Service.Interface.Business
{
    public interface ISettingBusiness
    {
        T GetSetting<T>(string settingName, T defaultValue);
        IEnumerable<Entity.Setting> GetSettings();
    }
}