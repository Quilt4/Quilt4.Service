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
        private readonly ISettingBusiness _settingBusiness;

        public ClientSessionController(ISessionBusiness sessionBusiness, ISettingBusiness settingBusiness)
        {
            _sessionBusiness = sessionBusiness;
            _settingBusiness = settingBusiness;
        }

        [AllowAnonymous]
        [Route("api/Client/Session")]
        public SessionResponse Post([FromBody] object value)
        {
            var sessionRequest = value.ToSessionRequest();
            var data = sessionRequest.ToSessionRequestEntity(HttpContext.Current.Request.UserHostAddress);
            var response = _sessionBusiness.RegisterSession(data);

            //TODO: Ither get the correct data here. Or provide an API that only needs the actual session key to get the correct result on the webpage
            var prokectKey = "3fa9f7de-3340-4675-bf9b-9fdf4091d6b3";
            var applicationKey = "a5baeb19-d56f-407b-8b8f-9a78ae461848";
            var versionKey = "14742426-1993-4d47-8040-c8ce85b02335";

            return new SessionResponse
            {
                SessionKey = response.SessionKey,
                ServerStartTime = response.ServerStartTime,
                //TODO: Append a correct path here
                SessionUrl = _settingBusiness.GetSetting("WebUrl", Request.RequestUri.AbsoluteUri) + string.Format("/#/project/{0}/application/{1}/version/{2}", prokectKey, applicationKey, versionKey)
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