using Quilt4.Service.Entity;
using Tharga.Quilt4Net.DataTransfer;

namespace Quilt4.Service.Converters
{
    public static class RegisterSessionConverter
    {
        public static SessionRequestEntity ToSessionRequestEntity(this RegisterSessionRequest item, string callerIp)
        {
            if (item == null)
                return null;

            return new SessionRequestEntity
            {
                SessionKey = item.SessionKey,
                ProjectApiKey = item.ProjectApiKey,
                ClientStartTime = item.ClientStartTime,
                Environment = item.Environment,
                Application = item.Application.ToApplicationDataRequestEntity(),
                Machine = item.Machine.ToMachineDataRequestEntity(),
                User = item.User.ToUserDataRequestEntity(),
                CallerIp = callerIp,
            };
        }

        public static ApplicationDataRequestEntity ToApplicationDataRequestEntity(this ApplicationData item)
        {
            if (item == null)
                return null;

            return new ApplicationDataRequestEntity
            {
                Fingerprint = item.Fingerprint,
                Name = item.Name,
                Version = item.Version,
                SupportToolkitNameVersion = item.SupportToolkitNameVersion,
                BuildTime = item.BuildTime
            };
        }

        public static MachineDataRequestEntity ToMachineDataRequestEntity(this MachineData item)
        {
            if (item == null)
                return null;

            return new MachineDataRequestEntity
            {
                Fingerprint = item.Fingerprint,
                Name = item.Name,
                Data = item.Data
            };
        }

        public static UserDataRequestEntity ToUserDataRequestEntity(this UserData item)
        {
            if (item == null)
                return null;

            return new UserDataRequestEntity
            {
                Fingerprint = item.Fingerprint,
                UserName = item.UserName
            };
        }
    }
}