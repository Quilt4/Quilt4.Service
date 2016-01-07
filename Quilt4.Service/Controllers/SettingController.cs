using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Quilt4.Service.Interface.Business;
using Quilt4Net.Core.DataTransfer;

namespace Quilt4.Service.Controllers
{
    [Authorize(Roles = Constants.Administrators)]
    public class SettingController : ApiController
    {
        private readonly ISettingBusiness _settingBusiness;

        public SettingController(ISettingBusiness settingBusiness)
        {
            _settingBusiness = settingBusiness;
        }

        [Route("api/Setting")]
        public IEnumerable<SettingResponse> Get()
        {
            return _settingBusiness.GetSettings().Select(x => new SettingResponse { Name = x.Name, Value = x.Value });
        }

        [Route("api/Setting/{id}")]
        public SettingResponse Get(string id)
        {
            var value = _settingBusiness.GetSetting<string>(id);
            var response = new SettingResponse { Name = id, Value = value };
            return response;
        }

        [Route("api/Setting")]
        public void Post([FromBody]SettingResponse value)
        {
            _settingBusiness.SetSetting<string>(value.Name, value.Value);
        }

        [Route("api/Setting/{id}")]
        public void Put(string id, [FromBody]SettingResponse value)
        {
            _settingBusiness.SetSetting<string>(id, value.Value);
        }

        [Route("api/Setting/{id}")]
        public void Delete(string id)
        {
            _settingBusiness.DeleteSetting(id);
        }
    }
}