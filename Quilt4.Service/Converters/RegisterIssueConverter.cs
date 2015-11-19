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
            };
        }

        public static RegisterIssueRequestEntity ToRegisterIssueRequestEntity(this RegisterIssueRequest item)
        {
            if (item == null)
                return null;

            return new RegisterIssueRequestEntity
            {
                Id = Guid.Parse(item.Id),
                SessionId = Guid.Parse(item.SessionId),
                ClientTime = item.ClientTime,
                Data = item.Data,
                IssueType = item.IssueType.ToIssueTypeRequestEntity(),
                IssueThreadId = string.IsNullOrEmpty(item.IssueThreadId) ? (Guid?) null : Guid.Parse(item.IssueThreadId),
                UserHandle = item.UserHandle,
                UserInput = item.UserInput,
                ClientToken = item.ClientToken
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
    }
}