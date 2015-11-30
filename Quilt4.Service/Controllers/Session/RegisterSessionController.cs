using System;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Quilt4.Service.Business.Handlers.Commands;
using Quilt4.Service.Converters;
using Quilt4.Service.Entity;
using Tharga.Quilt4Net.DataTransfer;

namespace Quilt4.Service.Controllers.Session
{
    public class RegisterSessionController : ApiController
    {
        private readonly RegisterSessionCommandHandler _handler;

        public RegisterSessionController(RegisterSessionCommandHandler handler)
        {
            _handler = handler;
        }

        [HttpPost]
        [Route("api/Session/Register")]
        public void CreateProject(RegisterSessionRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request), "No request object provided.");

            var data = GetData(request).ToSessionRequestEntity(HttpContext.Current.Request.UserHostAddress);
            _handler.StartHandle(data);
        }

        private RegisterSessionRequest GetData(object request)
        {
            RegisterSessionRequest data;
            try
            {
                var requestString = JsonConvert.SerializeObject(request);
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