using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Quil4.Service.Interface.Business;
using Quilt4.Service.Converters;
using Quilt4.Service.DataTransfer;

namespace Quilt4.Service.Controllers.ClientApi
{
    public class SessionController : ApiController
    {
        private readonly ISessionBusiness _sessionBusiness;
        public SessionController(ISessionBusiness sessionBusiness)
        {
            _sessionBusiness = sessionBusiness;
        }

        [HttpPost]
        [Route("api/session/register")]
        [AllowAnonymous]
        public void RegisterSession([FromBody] object request)
        {
            if (request == null)
                throw new ArgumentNullException("request", "No request object provided.");

            try
            {
                var data = GetData(request).ToSessionRequestEntity(HttpContext.Current.Request.UserHostAddress);

                _sessionBusiness.RegisterSession(data);
            }
            catch (Exception exception)
            {
                //TODO: Log exception
                throw;
            }
        }

        private RegisterSessionRequest GetData(object request)
        {
            var requestString = request.ToString();

            RegisterSessionRequest data;
            try
            {
                data = JsonConvert.DeserializeObject<RegisterSessionRequest>(requestString);
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
