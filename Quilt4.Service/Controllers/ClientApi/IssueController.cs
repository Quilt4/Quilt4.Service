using System;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Quil4.Service.Interface.Business;
using Quilt4.Service.Converters;
using Quilt4.Service.DataTransfer;

namespace Quilt4.Service.Controllers.ClientApi
{
    public class IssueController : ApiController
    {
        private readonly IIssueBusiness _issueBusiness;
        public IssueController(IIssueBusiness issueBusiness)
        {
            _issueBusiness = issueBusiness;
        }

        [HttpPost]
        [Route("api/issue/register")]
        [AllowAnonymous]
        public RegisterIssueResponse RegisterIssue([FromBody] object request)
        {
            if (request == null)
                throw new ArgumentNullException("request", "No request object provided.");

            try
            {
                var data = GetData(request);
                var registerIssueResponse = _issueBusiness.RegisterIssue(data.ToRegisterIssueRequestEntity(HttpContext.Current.Request.UserHostAddress)).ToRegisterIssueResponse();

                return registerIssueResponse;
            }
            catch (Exception exception)
            {
                //TODO: Log exception
                throw;
            }
        }

        private RegisterIssueRequest GetData(object request)
        {
            var requestString = request.ToString();

            RegisterIssueRequest data;
            try
            {
                data = JsonConvert.DeserializeObject<RegisterIssueRequest>(requestString);
            }
            catch (Exception exception)
            {
                //TODO: Log exception
                throw;
            }

            return data;
        }
    }
}