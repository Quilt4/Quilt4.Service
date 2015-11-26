using System;
using Quilt4.Service.DataTransfer;
using Quilt4.Service.Entity;

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
                ClientToken = item.ClientToken,
                SessionId = Guid.Parse(item.SessionId),
                ClientStartTime = item.ClientStartTime,
                Environment = item.Environment,
                Application = item.Application.ToApplicationDataRequestEntity(),
                Machine = item.Machine.ToMachineDataRequestEntity(),
                User = item.User.ToUserDataRequestEntity(),
                CallerIp = callerIp,
            };
        }

        public static ApplicationDataRequestEntity ToApplicationDataRequestEntity(this ApplicationDataRequest item)
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

        public static MachineDataRequestEntity ToMachineDataRequestEntity(this MachineDataRequest item)
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

        public static UserDataRequestEntity ToUserDataRequestEntity(this UserDataRequest item)
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