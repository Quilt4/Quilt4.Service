using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using Quilt4.Service.Converters;
using Quilt4.Service.Interface.Business;
using Quilt4Net.Core.DataTransfer;

namespace Quilt4.Service.Controllers.ClientApi
{
    [Authorize]
    public class ClientIssueController : ApiController
    {
        private readonly IIssueBusiness _issueBusiness;

        public ClientIssueController(IIssueBusiness issueBusiness)
        {
            _issueBusiness = issueBusiness;
        }

        [Route("api/Client/Issue")]
        public IEnumerable<object> Get() //TOOD: Replace object with IssueData from the nuget package
        {
            throw new NotImplementedException();
        }

        [Route("api/Client/Issue/{id}")]
        public object Get(Guid id) //TOOD: Replace object with IssueData from the nuget package
        {
            throw new NotImplementedException();
        }

        [AllowAnonymous]
        [Route("api/Client/Issue")]
        public IssueResponse Post([FromBody] object value)
        {
            var data = value.ToIssueRequest().ToRegisterIssueRequestEntity(HttpContext.Current.Request.UserHostAddress);
            var response = _issueBusiness.RegisterIssue(data);
            //return new IssueResponse {};
            throw new NotImplementedException();
        }

        //        [HttpPost]
        //        [Route("api/issue/register")]
        //        [AllowAnonymous]
        //        public RegisterIssueResponse RegisterIssue([FromBody] object request)
        //        {
        //            if (request == null)
        //                throw new ArgumentNullException("request", "No request object provided.");

        //            try
        //            {
        //                var data = ToSessionRequest(request).ToRegisterIssueRequestEntity();
        //                var registerIssueResponse = _issueBusiness.RegisterIssue(data).ToRegisterIssueResponse();

        //                return registerIssueResponse;
        //            }
        //            catch (Exception exception)
        //            {
        //                //TODO: Log exception
        //                throw;
        //            }
        //        }

        //        private RegisterIssueRequest ToSessionRequest(object request)
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
    }
}