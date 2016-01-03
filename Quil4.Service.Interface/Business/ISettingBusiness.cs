using System.Collections.Generic;

namespace Quilt4.Service.Interface.Business
{
    public interface IEmailSender
    {
        void Send(string to, string subject, string body);
    }

    public interface ISettingBusiness
    {
        T GetSetting<T>(string settingName, T defaultValue);
        IEnumerable<Entity.Setting> GetSettings();
    }
}