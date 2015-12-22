using System;
using Quilt4.Service.Entity;
using Quilt4Net.Core.DataTransfer;

namespace Quilt4.Service.Converters
{
    internal static class EntityConverters
    {
        public static RegisterIssueRequestEntity ToRegisterIssueRequestEntity(this IssueRequest item, string callerIp)
        {
            throw new NotImplementedException();
        }

        public static RegisterSessionRequestEntity ToSessionRequestEntity(this SessionRequest item, string callerIp)
        {
            if (item == null)
                return null;

            return new RegisterSessionRequestEntity
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