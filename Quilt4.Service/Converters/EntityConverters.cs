﻿using Quilt4.Service.Entity;
using Tharga.Quilt4Net.DataTransfer;

namespace Quilt4.Service.Converters
{
    internal static class EntityConverters
    {
        public static SessionRequestEntity ToSessionRequestEntity(this SessionData item, string callerIp)
        {
            if (item == null)
                return null;

            return new SessionRequestEntity
            {
                ProjectApiKey = item.ProjectApiKey,
                SessionId = item.SessionKey,
                ClientStartTime = item.ClientStartTime,
                Environment = item.Environment,
                Application = item.Application.ToApplicationDataRequestEntity(),
                Machine = item.Machine.ToMachineDataRequestEntity(),
                User = item.User.ToUserDataRequestEntity(),
                CallerIp = callerIp,
            };
        }

        private static ApplicationDataRequestEntity ToApplicationDataRequestEntity(this ApplicationData item)
        {
            if (item == null)
                return null;

            return new ApplicationDataRequestEntity
            {
                Fingerprint = item.Fingerprint,
                Name = item.Name,
                Version = item.Version,
                SupportToolkitNameVersion = item.SupportToolkitNameVersion,
                BuildTime = item.BuildTime,
            };
        }

        private static MachineDataRequestEntity ToMachineDataRequestEntity(this MachineData item)
        {
            if (item == null)
                return null;

            return new MachineDataRequestEntity
            {
                Fingerprint = item.Fingerprint,
                Name = item.Name,
                Data = item.Data,
            };
        }

        private static UserDataRequestEntity ToUserDataRequestEntity(this UserData item)
        {
            if (item == null)
                return null;

            return new UserDataRequestEntity
            {
                Fingerprint = item.Fingerprint,
                UserName = item.UserName,
            };
        }
    }
}