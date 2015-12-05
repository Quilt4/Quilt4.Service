//using System;
//using System.Web.Http;
//using Newtonsoft.Json;
//using Quilt4.Service.Converters;
//using Quilt4.Service.DataTransfer;
//using Quilt4.Service.Interface.Business;

//namespace Quilt4.Service.Controllers.ClientApi
//{
//    public class IssueController : ApiController
//    {
//        private readonly IIssueBusiness _issueBusiness;
//        public IssueController(IIssueBusiness issueBusiness)
//        {
//            _issueBusiness = issueBusiness;
//        }

//        [HttpPost]
//        [Route("api/issue/register")]
//        [AllowAnonymous]
//        public RegisterIssueResponse RegisterIssue([FromBody] object request)
//        {
//            if (request == null)
//                throw new ArgumentNullException("request", "No request object provided.");

//            try
//            {
//                var data = ToSessionData(request).ToRegisterIssueRequestEntity();
//                var registerIssueResponse = _issueBusiness.RegisterIssue(data).ToRegisterIssueResponse();

//                return registerIssueResponse;
//            }
//            catch (Exception exception)
//            {
//                //TODO: Log exception
//                throw;
//            }
//        }

//        private RegisterIssueRequest ToSessionData(object request)
//        {
//            var requestString = request.ToString();

//            RegisterIssueRequest data;
//            try
//            {
//                data = JsonConvert.DeserializeObject<RegisterIssueRequest>(requestString);
//            }
//            catch (Exception exception)
//            {
//                //TODO: Log exception
//                throw;
//            }

//            return data;
//        }
//    }
//}