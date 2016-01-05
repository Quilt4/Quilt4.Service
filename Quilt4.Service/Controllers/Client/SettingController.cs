using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Quilt4.Service.Interface.Business;

namespace Quilt4.Service.Controllers.Client
{
    [Authorize(Roles = Constants.Administrators)]
    public class SettingController : ApiController
    {
        private readonly ISettingBusiness _settingBusiness;

        public SettingController(ISettingBusiness settingBusiness)
        {
            _settingBusiness = settingBusiness;
        }

        [Route("api/Client/Setting")]
        public IEnumerable<SettingResponse> Get()
        {
            return _settingBusiness.GetSettings().Select(x => new SettingResponse { Name = x.Name, Value = x.Value });
        }

        [Route("api/Client/Setting/{id}")]
        public SettingResponse Get(Guid id)
        {
            throw new NotImplementedException();
        }

        [Route("api/Client/Setting")]
        public void Post([FromBody]SettingResponse value)
        {
            throw new NotImplementedException();
        }

        [Route("api/Client/Setting/{id}")]
        public void Put(Guid id, [FromBody]SettingResponse value)
        {
            throw new NotImplementedException();
        }

        [Route("api/Client/Setting/{id}")]
        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}