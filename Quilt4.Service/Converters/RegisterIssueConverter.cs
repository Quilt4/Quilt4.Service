//using Quilt4.Service.Controllers.WebAPI.Web.DataTransfer;
//using Quilt4.Service.Entity;

//namespace Quilt4.Service.Converters
//{
//    public static class RegisterIssueConverter
//    {
//        public static RegisterIssueResponse ToRegisterIssueResponse(this RegisterIssueResponseEntity item)
//        {
//            if (item == null)
//                return null;

//            return new RegisterIssueResponse
//            {
//                Ticket = item.Ticket,
//            };
//        }

//        //public static RegisterIssueRequestEntity ToRegisterIssueRequestEntity(this Quilt4Net.Core.DataTransfer.IssueRequest item)
//        //{
//        //    if (item == null)
//        //        return null;

//        //    return new RegisterIssueRequestEntity
//        //    {
//        //        IssueKey = item.IssueKey,
//        //        SessionKey = item.SessionKey,
//        //        ClientTime = item.ClientTime,
//        //        //Data = item.Data,
//        //        //IssueType = item.IssueType.Select(x => x.ToIssueTypeRequestEntity()) , //.ToIssueTypeRequestEntity(),
//        //        IssueThreadId = string.IsNullOrEmpty(item.IssueThreadId) ? (Guid?) null : Guid.Parse(item.IssueThreadId),
//        //        UserHandle = item.UserHandle,
//        //        UserInput = item.UserInput,
//        //        //Level = item
//        //    };
//        //}

//        //public static IssueTypeRequestEntity ToIssueTypeRequestEntity(this IssueTypeRequest item)
//        //{
//        //    if (item == null)
//        //        return null; 

//        //    return new IssueTypeRequestEntity
//        //    {
//        //        Message = item.Message,
//        //        StackTrace = item.StackTrace,
//        //        IssueLevel = item.IssueLevel,
//        //        Type = item.Type,
//        //        Inner = item.Inner.ToIssueTypeRequestEntity()
//        //    };
//        //}
//    }
//}