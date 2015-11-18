using System;
using Quilt4.Service.DataTransfer;
using Quilt4.Service.Entity;

namespace Quilt4.Service.Converters
{
    public static class RegisterIssueConverter
    {
        public static RegisterIssueResponse ToRegisterIssueResponse(this RegisterIssueResponseEntity item)
        {
            if (item == null)
                return null;

            return new RegisterIssueResponse
            {
                Ticket = item.Ticket,
                ResponseMessage = item.ResponseMessage
            };
        }

        public static RegisterIssueRequestEntity ToRegisterIssueRequestEntity(this RegisterIssueRequest item)
        {
            if (item == null)
                return null;

            return new RegisterIssueRequestEntity
            {
                Id = Guid.Parse(item.Id),
                Session = item.Session.ToSessionRequestEntity(),
                ClientTime = item.ClientTime,
                Data = item.Data,
                IssueType = item.IssueType.ToIssueTypeRequestEntity(),
                IssueThreadId = string.IsNullOrEmpty(item.IssueThreadId) ? (Guid?) null : Guid.Parse(item.IssueThreadId),
                UserHandle = item.UserHandle,
                UserInput = item.UserInput
            };
        }

        public static IssueTypeRequestEntity ToIssueTypeRequestEntity(this IssueTypeRequest item)
        {
            if (item == null)
                return null; 

            return new IssueTypeRequestEntity
            {
                Message = item.Message,
                StackTrace = item.StackTrace,
                IssueLevel = item.IssueLevel,
                Type = item.Type,
                Inner = item.Inner.ToIssueTypeRequestEntity()
            };
        }

        public static SessionRequestEntity ToSessionRequestEntity(this SessionRequest item)
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
                User = item.User.ToUserDataRequestEntity()
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