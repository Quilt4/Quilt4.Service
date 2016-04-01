using System.Web;
using System.Web.Http;
using Quilt4.Service.Converters;
using Quilt4.Service.Interface.Business;
using Quilt4Net.Core.DataTransfer;

namespace Quilt4.Service.Controllers.WebAPI.Client
{
    [Authorize]
    public class ClientSessionController : ApiController
    {
        private readonly ISessionBusiness _sessionBusiness;

        public ClientSessionController(ISessionBusiness sessionBusiness)
        {
            _sessionBusiness = sessionBusiness;
        }

        [AllowAnonymous]
        [Route("api/Client/Session")]
        public SessionResponse Post([FromBody] object value)
        {
            var sessionRequest = value.ToSessionRequest();
            var data = sessionRequest.ToSessionRequestEntity(HttpContext.Current.Request.UserHostAddress);
            var response = _sessionBusiness.RegisterSession(data);
            return new SessionResponse
            {
                SessionKey = response.SessionKey,
                ServerStartTime = response.ServerStartTime,
                //TODO: Append a correct path here
                SessionUrl = "http://www.quilt4net.com/SomePathToSession"
            };
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/Client/Session/End")]
        public void End([FromBody] object value)
        {
            var sessionKey = value.ToString();
            var callerIp = HttpContext.Current.Request.UserHostAddress;
            _sessionBusiness.EndSession(sessionKey, callerIp);
        }
    }
}