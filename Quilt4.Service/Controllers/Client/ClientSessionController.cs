using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using Quilt4.Service.Converters;
using Quilt4.Service.Interface.Business;
using Tharga.Quilt4Net.DataTransfer;

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

        [Route("api/Client/Session")]
        public IEnumerable<SessionData> Get()
        {
            throw new NotImplementedException();
        }

        [Route("api/Client/Session/{id}")]
        public SessionData Get(Guid id)
        {
            throw new NotImplementedException();
        }

        [AllowAnonymous]
        [Route("api/Client/Session")]
        public void Post([FromBody] object value)
        {
            var data = value.ToSessionData().ToSessionRequestEntity(HttpContext.Current.Request.UserHostAddress);
            _sessionBusiness.RegisterSession(data);
        }
    }
}