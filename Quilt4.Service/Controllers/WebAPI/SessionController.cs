using System.Web;
using System.Web.Http;
using Quilt4.Service.Converters;
using Quilt4.Service.Interface.Business;
using Quilt4Net.Core.DataTransfer;

namespace Quilt4.Service.Controllers.WebAPI
{
    [Authorize]
    public class SessionController : ApiController
    {
        private readonly ISessionBusiness _sessionBusiness;
        private readonly ISettingBusiness _settingBusiness;

        public SessionController(ISessionBusiness sessionBusiness, ISettingBusiness settingBusiness)
        {
            _sessionBusiness = sessionBusiness;
            _settingBusiness = settingBusiness;
        }

        [AllowAnonymous]
        [Route("api/Session")]
        public SessionResponse Post([FromBody] object value)
        {
            var sessionRequest = value.ToSessionRequest();
            var data = sessionRequest.ToSessionRequestEntity(HttpContext.Current.Request.UserHostAddress);
            var response = _sessionBusiness.RegisterSession(data);

            return new SessionResponse
            {
                SessionKey = response.SessionKey,
                ServerStartTime = response.ServerStartTime,
                SessionUrl = _settingBusiness.GetSetting("WebUrl", Request.RequestUri.AbsoluteUri) + $"session/{response.SessionKey}"
            };
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/Session/End")]
        public void End([FromBody] object value)
        {
            var sessionKey = value.ToString();
            var callerIp = HttpContext.Current.Request.UserHostAddress;
            _sessionBusiness.EndSession(sessionKey, callerIp);
        }
    }
}