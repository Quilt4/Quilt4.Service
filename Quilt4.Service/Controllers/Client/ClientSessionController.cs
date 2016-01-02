using System;
using System.Web;
using System.Web.Http;
using Quilt4.Service.Converters;
using Quilt4.Service.Interface.Business;
using Quilt4Net.Core.DataTransfer;

namespace Quilt4.Service.Controllers.Client
{
    [Authorize]
    public class ClientSessionController : ApiController
    {
        private readonly ISessionBusiness _sessionBusiness;

        public ClientSessionController(ISessionBusiness sessionBusiness)
        {
            _sessionBusiness = sessionBusiness;
        }

        //[Route("api/Client/Session")]
        //public IEnumerable<SessionResponse> Get()
        //{
        //    throw new NotImplementedException();
        //}

        //[Route("api/Client/Session/{id}")]
        //public SessionResponse Get(Guid id)
        //{
        //    throw new NotImplementedException();
        //}

        [AllowAnonymous]
        [Route("api/Client/Session")]
        public SessionResponse Post([FromBody] object value)
        {
            var sessionRequest = value.ToSessionRequest();
            var data = sessionRequest.ToSessionRequestEntity(HttpContext.Current.Request.UserHostAddress);
            var response = _sessionBusiness.RegisterSession(data);
            return new SessionResponse
            {
                SessionKey = sessionRequest.SessionKey,
                Application = sessionRequest.Application,
                ClientEndTime = null,
                ClientStartTime = sessionRequest.ClientStartTime,
                Environment = sessionRequest.Environment,
                Machine = sessionRequest.Machine,
                ServerStartTime = response.ServerStartTime,
                User = sessionRequest.User,
            };
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/Client/Session/End")]
        public void End([FromBody] object value)
        {
            var sessionKey = Guid.Parse(value.ToString());
            var callerIp = HttpContext.Current.Request.UserHostAddress;
            _sessionBusiness.EndSession(sessionKey, callerIp);
        }
    }
}